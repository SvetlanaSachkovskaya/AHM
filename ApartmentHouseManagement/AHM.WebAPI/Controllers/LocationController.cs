using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using AHM.BusinessLayer.Interfaces;
using AHM.Common.DomainModel;

namespace AHM.WebAPI.Controllers
{
    [Authorize]
    [RoutePrefix("api/Location")]
    public class LocationController : BaseController
    {
        private readonly ILocationService _locationService;


        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }


        [HttpGet]
        [Route("GetAll")]
        public async Task<IHttpActionResult> GetAll()
        {
            var locations = await _locationService.GetAllLocationsAsync(AppUser.BuildingId ?? 0);

            return Ok(locations);
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IHttpActionResult> Add(Location location)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (AppUser.BuildingId.HasValue)
            {
                location.BuildingId = AppUser.BuildingId.Value;
            }

            var result = await _locationService.AddAsync(location);

            return result.IsSuccessful ? (IHttpActionResult)Ok(location) : BadRequest(result.Errors.First());
        }

        [HttpPost]
        [Route("Update")]
        public async Task<IHttpActionResult> Update(Location location)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _locationService.UpdateAsync(location);

            return result.IsSuccessful ? (IHttpActionResult)Ok(location) : BadRequest(result.Errors.First());
        }

        [HttpPost]
        [Route("Remove")]
        public async Task<IHttpActionResult> Remove(Location location)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _locationService.RemoveAsync(location.Id);

            return result.IsSuccessful ? (IHttpActionResult)Ok(location) : BadRequest(result.Errors.First());
        }
    }
}