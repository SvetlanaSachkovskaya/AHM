using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using AHM.BusinessLayer.Interfaces;
using AHM.Common.DomainModel;
using AHM.WebAPI.Attributes;

namespace AHM.WebAPI.Controllers
{
    [RoutePrefix("api/Occupant")]
    public class OccupantController : BaseController
    {
        private readonly IOccupantService _occupantService;


        public OccupantController(IOccupantService occupantService)
        {
            _occupantService = occupantService;
        }

        [Authorization(Roles = new[] { Roles.Manager, Roles.Concierge, Roles.Accountant })]
        [HttpGet]
        [Route("GetAll")]
        public async Task<IHttpActionResult> GetAll()
        {
            var occupants = await _occupantService.GetAllOccupantsAsync(AppUser.BuildingId ?? 0);
            return Ok(occupants);
        }

        [Authorization(Roles = new[] { Roles.Manager, Roles.Concierge, Roles.Accountant })]
        [HttpGet]
        [Route("GetById")]
        public async Task<IHttpActionResult> GetById(int id)
        {
            var occupant = await _occupantService.GetOccupantByIdAsync(id);
            return Ok(occupant);
        }

        [Authorization(Roles = new[] { Roles.Manager, Roles.Concierge, Roles.Accountant })]
        [HttpGet]
        [Route("GetByApartmentId")]
        public async Task<IHttpActionResult> GetByApartmentId(int apartmentId)
        {
            var occupants = await _occupantService.GetOccupantsByApartmentIdAsync(apartmentId);
            return Ok(occupants);
        }

        [Authorization(Roles = new[] { Roles.Manager, Roles.Concierge, Roles.Accountant })]
        [HttpGet]
        [Route("GetApartmentOwner")]
        public async Task<IHttpActionResult> GetApartmentOwner(int apartmentId)
        {
            var occupants = await _occupantService.GetApartmentOwnerAsync(apartmentId);            
            return Ok(occupants);
        }

        [Authorization(Roles = new[] { Roles.Manager })]
        [HttpPost]
        [Route("Add")]
        public async Task<IHttpActionResult> Add(Occupant occupant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _occupantService.AddAsync(occupant);

            return result.IsSuccessful ? (IHttpActionResult)Ok(occupant) : BadRequest(result.Errors.First());
        }

        [Authorization(Roles = new[] { Roles.Manager })]
        [HttpPost]
        [Route("Update")]
        public async Task<IHttpActionResult> Update(Occupant occupant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _occupantService.UpdateAsync(occupant);

            return result.IsSuccessful ? (IHttpActionResult)Ok(occupant) : BadRequest(result.Errors.First());
        }

        [Authorization(Roles = new[] { Roles.Manager })]
        [HttpPost]
        [Route("Remove")]
        public async Task<IHttpActionResult> Remove(Occupant occupant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _occupantService.RemoveAsync(occupant.Id);

            return result.IsSuccessful ? (IHttpActionResult)Ok(occupant) : BadRequest(result.Errors.First());
        }
    }
}