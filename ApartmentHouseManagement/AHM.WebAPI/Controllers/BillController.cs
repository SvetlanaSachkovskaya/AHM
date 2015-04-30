using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using AHM.BusinessLayer.Interfaces;
using AHM.Common;
using AHM.Common.DomainModel;
using AHM.Common.Helpers;
using AHM.WebAPI.Attributes;
using AHM.WebAPI.Models;

namespace AHM.WebAPI.Controllers
{
    [Authorization(Roles = new[] { Roles.Accountant })]
    [RoutePrefix("api/Bill")]
    public class BillController : BaseController
    {
        private readonly IBillService _billService;
        private readonly IUtilitiesItemService _utilitiesItemService;
        private readonly IBillPdfGenerator _billPdfGenerator;


        public BillController(
            IBillService billService, 
            IUtilitiesItemService utilitiesItemService,
            IBillPdfGenerator billPdfGenerator)
        {
            _billService = billService;
            _utilitiesItemService = utilitiesItemService;
            _billPdfGenerator = billPdfGenerator;
        }


        [HttpGet]
        [Route("GetAll")]
        public async Task<IHttpActionResult> GetAll(int dateInterval)
        {
            var bills = await _billService.GetAllBillsAsync(AppUser.BuildingId ?? 0, (BillDateInteval) dateInterval);

            return Ok(bills.OrderByDescending(b => b.Date));
        }

        [HttpGet]
        [Route("GetUnpaid")]
        public async Task<IHttpActionResult> GetUnpaid(int dateInterval)
        {
            var bills = await _billService.GetAllBillsAsync(AppUser.BuildingId ?? 0, (BillDateInteval)dateInterval, false);

            return Ok(bills.OrderByDescending(b => b.Date));
        }

        [HttpGet]
        [Route("GetBillDateIntervals")]
        public async Task<IHttpActionResult> GetBillDateIntervals()
        {
            var dateIntervals = new EnumCollection<BillDateInteval>();

            return Ok(dateIntervals);
        }

        [HttpGet]
        [Route("GetFullById")]
        public async Task<IHttpActionResult> GetFullById(int id)
        {
            var bill = await _billService.GetByIdAsync(id);
            var utilitiesItems = await _utilitiesItemService.GetByBillIdAsync(id);

            return Ok(new BillModel(bill, utilitiesItems.ToList()));
        }

        [HttpGet]
        [Route("GetShortById")]
        public async Task<IHttpActionResult> GetShortById(int id)
        {
            var bill = await _billService.GetByIdAsync(id);

            return Ok(bill);
        }

        [HttpGet]
        [Route("GetBillPdfPath")]
        public async Task<IHttpActionResult> GetBillPdfPath(int billId)
        {
            var fileName = await _billPdfGenerator.GenerateAsync(billId, GetPdfFolderPath());

            var fileRelativePath = ConfigurationManager.AppSettings["BillsDirectory"] + @"/" + fileName;
            return Ok(fileRelativePath);
        }

        [HttpPost]
        [Route("SendEmail")]
        public async Task<IHttpActionResult> SendEmail(Bill bill)
        {
            var email = ConfigurationManager.AppSettings["Email"];
            var username = ConfigurationManager.AppSettings["Username"];
            var password = ConfigurationManager.AppSettings["Password"];

            var pdfFolder = GetPdfFolderPath();
            var fileName = await _billPdfGenerator.GenerateAsync(bill.Id, pdfFolder);
            var filePath = Path.Combine(pdfFolder, fileName);

            var result = await _billService.SendEmailAsync(bill, email, username, password, filePath);

            return result.IsSuccessful ? (IHttpActionResult)Ok() : BadRequest(result.Errors.First());
        }
        
        [HttpPost]
        [Route("Add")]
        public async Task<IHttpActionResult> Add(BillModel bill)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var billEntity = bill.GetBill();
            var result = await _billService.AddAsync(billEntity, bill.UtilitiesItems);

            return result.IsSuccessful ? (IHttpActionResult)Ok(billEntity) : BadRequest(result.Errors.First());
        }

        [HttpPost]
        [Route("Update")]
        public async Task<IHttpActionResult> Update(BillModel bill)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result =  await _billService.UpdateAsync(bill.GetBill(), bill.UtilitiesItems);

            return result.IsSuccessful ? (IHttpActionResult)Ok() : BadRequest(result.Errors.First());
        }

        [HttpPost]
        [Route("Pay")]
        public async Task<IHttpActionResult> Pay(Bill bill)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _billService.PayBillAsync(bill);

            return result.IsSuccessful ? (IHttpActionResult)Ok() : BadRequest(result.Errors.First());
        }

        private string GetPdfFolderPath()
        {
            var directoryRelativePath = ConfigurationManager.AppSettings["BillsDirectory"];
            var uiProjectPhysicalPath = Path.Combine(Directory.GetParent(HttpContext.Current.Request.PhysicalApplicationPath).Parent.FullName, "AHM.UI");
            return Path.Combine(uiProjectPhysicalPath, directoryRelativePath);
        }
    }
}