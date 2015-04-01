using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using AHM.BusinessLayer.Interfaces;
using AHM.Common.DomainModel;

namespace AHM.WebAPI.Controllers
{
    [Authorize]
    [RoutePrefix("api/Journal")]
    public class JournalController : BaseController
    {
        private readonly IJournalService _journalService;


        public JournalController(IJournalService journalService)
        {
            _journalService = journalService;
        }

        [Authorize(Roles = "Concierge,Manager")]
        [HttpGet]
        [Route("GetAllActive")]
        public async Task<IHttpActionResult> GetAllActive()
        {
            var events = await _journalService.GetAllActiveEventsAsync(AppUser.BuildingId ?? 0);

            return Ok(events);
        }

        [Authorize(Roles = "Concierge,Manager")]
        [HttpGet]
        [Route("GetEventsPerDay")]
        public async Task<IHttpActionResult> GetEventsPerDay()
        {
            var events = await _journalService.GetEventsPerDay(AppUser.BuildingId ?? 0);

            return Ok(events);
        }

        [Authorize(Roles = "Concierge,Manager")]
        [HttpGet]
        [Route("GetEventsPerWeek")]
        public async Task<IHttpActionResult> GetEventsPerWeek()
        {
            var events = await _journalService.GetEventsPerWeek(AppUser.BuildingId ?? 0);

            return Ok(events);
        }

        [Authorize(Roles = "Concierge,Manager")]
        [HttpGet]
        [Route("GetEventsPerMonth")]
        public async Task<IHttpActionResult> GetEventsPerMonth()
        {
            var events = await _journalService.GetEventsPerMonth(AppUser.BuildingId ?? 0);

            return Ok(events);
        }

        [Authorize(Roles = "Concierge,Manager")]
        [HttpGet]
        [Route("GetEventsPerYear")]
        public async Task<IHttpActionResult> GetEventsPerYear()
        {
            var events = await _journalService.GetEventsPerYear(AppUser.BuildingId ?? 0);

            return Ok(events);
        }

        [Authorize(Roles = "Concierge")]
        [HttpPost]
        [Route("Add")]
        public async Task<IHttpActionResult> Add(Event ev)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (AppUser.BuildingId.HasValue)
            {
                ev.BuildingId = AppUser.BuildingId.Value;
            }

            var result = await _journalService.AddAsync(ev);

            return result.IsSuccessful ? (IHttpActionResult) Ok(ev) : BadRequest(result.Errors.First());
        }

        [Authorize(Roles = "Concierge")]
        [HttpPost]
        [Route("Update")]
        public async Task<IHttpActionResult> Update(Event ev)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _journalService.UpdateAsync(ev);

            return result.IsSuccessful ? (IHttpActionResult)Ok(ev) : BadRequest(result.Errors.First());
        }

        [Authorize(Roles = "Concierge")]
        [HttpPost]
        [Route("Remove")]
        public async Task<IHttpActionResult> Remove(Event ev)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ev.IsRemoved = true;

            var result = await _journalService.UpdateAsync(ev);

            return result.IsSuccessful ? (IHttpActionResult)Ok(ev) : BadRequest(result.Errors.First());
        }
    }
}