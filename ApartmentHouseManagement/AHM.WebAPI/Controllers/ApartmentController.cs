using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using AHM.BusinessLayer.Interfaces;
using AHM.Common;
using AHM.Common.DomainModel;
using AHM.WebAPI.Attributes;
using AHM.WebAPI.Models;

namespace AHM.WebAPI.Controllers
{
    [RoutePrefix("api/Apartment")]
    public class ApartmentController : BaseController
    {
        private readonly IApartmentService _apartmentService;


        public ApartmentController(IApartmentService apartmentService)
        {
            _apartmentService = apartmentService;
        }

        [Authorization(Roles = new []{Roles.Manager, Roles.Concierge, Roles.Accountant})]
        [HttpGet]
        [Route("GetAll")]
        public async Task<IHttpActionResult> GetAll()
        {
            var apartments =
                await
                    _apartmentService.GetAllApartmentsAsync(AppUser.BuildingId ?? 0);

            return Ok(apartments);
        }

        [Authorization(Roles = new[] { Roles.Manager, Roles.Concierge, Roles.Accountant })]
        [HttpGet]
        [Route("GetById")]
        public async Task<IHttpActionResult> GetById(int id)
        {
            var apartments =
                await
                    _apartmentService.GetApartmentByIdAsync(id);

            return Ok(apartments);
        }

        [Authorization(Roles = new[] { Roles.Manager })]
        [HttpPost]
        [Route("Add")]
        public async Task<IHttpActionResult> Add(EditApartmentModel apartment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.SelectMany(m => m.Value.Errors).First().ErrorMessage);
            }
            if (AppUser.BuildingId.HasValue)
            {
                apartment.BuildingId = AppUser.BuildingId.Value;
            }

            var result = await _apartmentService.AddAsync(apartment.GetApartment());

            return result.IsSuccessful ? (IHttpActionResult) Ok(apartment) : BadRequest(result.Errors.First());
        }

        [Authorization(Roles = new[] { Roles.Manager })]
        [HttpPost]
        [Route("Update")]
        public async Task<IHttpActionResult> Update(EditApartmentModel apartment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.SelectMany(m => m.Value.Errors).First().ErrorMessage);
            }

            var result = await _apartmentService.UpdateAsync(apartment.GetApartment(), apartment.OwnerId);

            return result.IsSuccessful ? (IHttpActionResult)Ok(apartment) : BadRequest(result.Errors.First());
        }

        [Authorization(Roles = new[] { Roles.Manager })]
        [HttpPost]
        [Route("Remove")]
        public async Task<IHttpActionResult> Remove(Apartment apartment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.SelectMany(m => m.Value.Errors).First().ErrorMessage);
            }

            var inUse = await _apartmentService.InUseAsync(apartment.Id);
            if (inUse)
            {
                return BadRequest(ValidationMessages.ApartmentInUse);
            }

            var result = await _apartmentService.RemoveAsync(apartment.Id);

            return result.IsSuccessful ? (IHttpActionResult)Ok(apartment) : BadRequest(result.Errors.First());
        }
    }
}