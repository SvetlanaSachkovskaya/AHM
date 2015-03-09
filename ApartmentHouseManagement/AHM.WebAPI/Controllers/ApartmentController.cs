using System.Threading.Tasks;
using System.Web.Http;
using AHM.BusinessLayer.Interfaces;
using AHM.Common.DomainModel;
using AHM.WebAPI.Models;

namespace AHM.WebAPI.Controllers
{
    [Authorize]
    [RoutePrefix("api/Apartment")]
    public class ApartmentController : BaseController
    {
        private readonly IApartmentService _apartmentService;


        public ApartmentController(IApartmentService apartmentService)
        {
            _apartmentService = apartmentService;
        }


        [HttpGet]
        [Route("GetAll")]
        public async Task<IHttpActionResult> GetAll()
        {
            var apartments =
                await
                    _apartmentService.GetAllApartmentsAsync(AppUser.BuildingId ?? 0);

            return Ok(apartments);
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IHttpActionResult> GetById(int id)
        {
            var apartments =
                await
                    _apartmentService.GetApartmentByIdAsync(id);

            return Ok(apartments);
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IHttpActionResult> Add(EditApartmentModel apartment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (AppUser.BuildingId.HasValue)
            {
                apartment.BuildingId = AppUser.BuildingId.Value;
            }

            await _apartmentService.AddAsync(apartment.GetApartment());

            return Ok(apartment);
        }

        [HttpPost]
        [Route("Update")]
        public async Task<IHttpActionResult> Update(EditApartmentModel apartment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _apartmentService.UpdateAsync(apartment.GetApartment(), apartment.OwnerId);

            return Ok();
        }

        [HttpPost]
        [Route("Remove")]
        public async Task<IHttpActionResult> Remove(Apartment apartment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _apartmentService.RemoveAsync(apartment.Id);

            return Ok();
        }
    }
}