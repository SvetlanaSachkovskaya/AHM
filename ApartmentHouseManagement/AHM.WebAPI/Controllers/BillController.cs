using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using AHM.BusinessLayer.Interfaces;
using AHM.Common;
using AHM.Common.Helpers;
using AHM.WebAPI.Models;

namespace AHM.WebAPI.Controllers
{
    [Authorize]
    [RoutePrefix("api/Bill")]
    public class BillController : BaseController
    {
        private readonly IBillService _billService;
        private readonly IUtilitiesItemService _utilitiesItemService;


        public BillController(IBillService billService, IUtilitiesItemService utilitiesItemService)
        {
            _billService = billService;
            _utilitiesItemService = utilitiesItemService;
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

        [HttpPost]
        [Route("Add")]
        public async Task<IHttpActionResult> Add(BillModel bill)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _billService.AddAsync(bill.GetBill(), bill.UtilitiesItems);

            return Ok(bill);
        }

        [HttpPost]
        [Route("Update")]
        public async Task<IHttpActionResult> Update(BillModel bill)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _billService.UpdateAsync(bill.GetBill(), bill.UtilitiesItems);

            return Ok();
        }
    }
}