using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using AHM.BusinessLayer.Interfaces;
using AHM.Common;
using AHM.Common.DomainModel;
using AHM.Common.Helpers;
using AHM.WebAPI.Models;

namespace AHM.WebAPI.Controllers
{
    [Authorize(Roles = "Accountant")]
    [RoutePrefix("api/Bill")]
    public class BillController : BaseController
    {
        private readonly IBillService _billService;
        private readonly IUtilitiesItemService _utilitiesItemService;
        private readonly IBillPdfGenerator _billPdfGenerator;
        private readonly IOccupantService _occupantService;


        public BillController(
            IBillService billService, 
            IUtilitiesItemService utilitiesItemService,
            IBillPdfGenerator billPdfGenerator,
            IOccupantService occupantService)
        {
            _billService = billService;
            _utilitiesItemService = utilitiesItemService;
            _billPdfGenerator = billPdfGenerator;
            _occupantService = occupantService;
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
        [Route("GetById")]
        public async Task<IHttpActionResult> GetById(int id)
        {
            var bill = await _billService.GetByIdAsync(id);
            var utilitiesItems = await _utilitiesItemService.GetByBillIdAsync(id);

            return Ok(new BillModel(bill, utilitiesItems.ToList()));
        }

        [HttpGet]
        [Route("GetBillPdfPath")]
        public async Task<IHttpActionResult> GetBillPdfPath(int billId)
        {
            var directoryRelativePath = ConfigurationManager.AppSettings["BillsDirectory"];
            var directory = Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, directoryRelativePath);
            var fileName = await _billPdfGenerator.GenerateAsync(billId, directory);

            var fileRelativePath = ConfigurationManager.AppSettings["BillsDirectory"] + @"/" + fileName;
            var url = String.Format("{0}/{1}/{2}", HttpContext.Current.Request.Url.Authority, directoryRelativePath,
                fileName);
            return Ok(url);
        }

        [HttpPost]
        [Route("SendEmail")]
        public async Task<IHttpActionResult> SendEmail(Bill bill)
        {
            var email = ConfigurationManager.AppSettings["Email"];
            var username = ConfigurationManager.AppSettings["Username"];
            var password = ConfigurationManager.AppSettings["Password"];

            var directoryRelativePath = ConfigurationManager.AppSettings["BillsDirectory"];
            var directory = Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, directoryRelativePath);
            var filePath = await _billPdfGenerator.GenerateAsync(bill.Id, directory);

            var owner = await _occupantService.GetApartmentOwnerAsync(bill.ApartmentId);

            var mail = new MailMessage(email, owner.Email);
            var client = new SmtpClient
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                Timeout = 10000,
                Credentials = new NetworkCredential(username, password)
            };

            mail.Subject = "Utilities bill";
            mail.Attachments.Add(new Attachment(filePath));
            client.Send(mail);

            bill.IsEmailSent = true;
            var result = await _billService.UpdateAsync(bill);

            return result.IsSuccessful ? (IHttpActionResult) Ok() : BadRequest(result.Errors.First());
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
    }
}