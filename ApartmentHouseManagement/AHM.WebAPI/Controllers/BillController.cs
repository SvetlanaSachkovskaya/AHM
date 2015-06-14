using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using AHM.BusinessLayer.Interfaces;
using AHM.Common.DomainModel;
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
        [Route("GetAllBills")]
        public async Task<IHttpActionResult> GetAllBills(bool showOnlyUnpaid)
        {
            var bills = await _billService.GetAllBillsAsync(AppUser.BuildingId ?? 0, showOnlyUnpaid);

            return Ok(bills.OrderByDescending(b => b.Date));
        }

        [HttpGet]
        [Route("GetBillsByDate")]
        public async Task<IHttpActionResult> GetBillsByDate(DateTime date)
        {
            var bills = await _billService.GetBillsByDateAsync(AppUser.BuildingId ?? 0, date);

            return Ok(bills.OrderByDescending(b => b.Date));
        }

        [HttpGet]
        [Route("GetUnpaidBillsByDate")]
        public async Task<IHttpActionResult> GetUnpaidBillsByDate(DateTime date)
        {
            var bills = await _billService.GetBillsByDateAsync(AppUser.BuildingId ?? 0, date, true);

            return Ok(bills.OrderByDescending(b => b.Date));
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
            var pdfFolder = GetPdfFolderPath();
            var fileName = await _billPdfGenerator.GenerateAsync(bill.Id, pdfFolder);
            var filePath = Path.Combine(pdfFolder, fileName);

            var result = await _billService.SendEmailAsync(bill, filePath);

            return result.IsSuccessful ? (IHttpActionResult)Ok() : BadRequest(result.Errors.First());
        }
        
        [HttpPost]
        [Route("Add")]
        public async Task<IHttpActionResult> Add(BillModel bill)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.SelectMany(m => m.Value.Errors).First().ErrorMessage);
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
                return BadRequest(ModelState.SelectMany(m => m.Value.Errors).First().ErrorMessage);
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
                return BadRequest(ModelState.SelectMany(m => m.Value.Errors).First().ErrorMessage);
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