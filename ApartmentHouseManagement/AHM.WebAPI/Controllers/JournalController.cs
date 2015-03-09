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


        [HttpGet]
        [Route("GetAll")]
        public async Task<IHttpActionResult> GetAll()
        {
            var events = await _journalService.GetAllEventsAsync(AppUser.BuildingId ?? 0);

            return Ok(events);
        }

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

            await _journalService.AddAsync(ev);

            return Ok(ev);
        }

        [HttpPost]
        [Route("Update")]
        public async Task<IHttpActionResult> Update(Event ev)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _journalService.UpdateAsync(ev);

            return Ok();
        }

        [HttpPost]
        [Route("Remove")]
        public async Task<IHttpActionResult> Remove(Event ev)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ev.IsRemoved = true;

            await _journalService.UpdateAsync(ev);

            return Ok();
        }
    }
}