using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using AHM.BusinessLayer.Interfaces;
using AHM.Common.DomainModel;
using AHM.WebAPI.Attributes;
using AHM.WebAPI.Models;

namespace AHM.WebAPI.Controllers
{
    [RoutePrefix("api/Journal")]
    public class JournalController : BaseController
    {
        private readonly IJournalService _journalService;


        public JournalController(IJournalService journalService)
        {
            _journalService = journalService;
        }

        [Authorization(Roles = new[] { Roles.Manager, Roles.Concierge })]
        [HttpGet]
        [Route("GetAllActive")]
        public async Task<IHttpActionResult> GetAllActive()
        {
            var events = await _journalService.GetAllActiveEventsAsync(AppUser.BuildingId ?? 0);

            return Ok(events);
        }

        [Authorization(Roles = new[] { Roles.Manager, Roles.Concierge })]
        [HttpGet]
        [Route("GetEventsByDate")]
        public async Task<IHttpActionResult> GetEventsPerDay(DateTime date)
        {
            var events = await _journalService.GetEventsByDate(date, AppUser.BuildingId ?? 0);

            return Ok(events);
        }

        [Authorization(Roles = new[] { Roles.Concierge })]
        [HttpPost]
        [Route("Add")]
        public async Task<IHttpActionResult> Add(EventModel eventModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.SelectMany(m => m.Value.Errors).First().ErrorMessage);
            }

            var newEvent = eventModel.GetEvent();
            if (AppUser.BuildingId.HasValue)
            {
                newEvent.BuildingId = AppUser.BuildingId.Value;
            }

            var result = await _journalService.AddAsync(newEvent);

            return result.IsSuccessful ? (IHttpActionResult)Ok(newEvent) : BadRequest(result.Errors.First());
        }

        [Authorization(Roles = new[] { Roles.Concierge })]
        [HttpPost]
        [Route("Update")]
        public async Task<IHttpActionResult> Update(Event ev)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.SelectMany(m => m.Value.Errors).First().ErrorMessage);
            }

            var result = await _journalService.UpdateAsync(ev);

            return result.IsSuccessful ? (IHttpActionResult)Ok(ev) : BadRequest(result.Errors.First());
        }

        [Authorization(Roles = new[] { Roles.Concierge })]
        [HttpPost]
        [Route("Remove")]
        public async Task<IHttpActionResult> Remove(Event ev)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.SelectMany(m => m.Value.Errors).First().ErrorMessage);
            }

            ev.IsRemoved = true;

            var result = await _journalService.UpdateAsync(ev);

            return result.IsSuccessful ? (IHttpActionResult)Ok(ev) : BadRequest(result.Errors.First());
        }
    }
}