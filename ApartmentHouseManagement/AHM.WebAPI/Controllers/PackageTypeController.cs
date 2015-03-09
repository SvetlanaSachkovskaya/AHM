using System.Threading.Tasks;
using System.Web.Http;
using AHM.BusinessLayer.Interfaces;
using AHM.Common.DomainModel;

namespace AHM.WebAPI.Controllers
{
    [Authorize]
    [RoutePrefix("api/PackageType")]
    public class PackageTypeController : BaseController
    {
        private readonly IPackageTypeService _packageTypeService;


        public PackageTypeController(IPackageTypeService packageTypeService)
        {
            _packageTypeService = packageTypeService;
        }


        [HttpGet]
        [Route("GetAll")]
        public async Task<IHttpActionResult> GetAll()
        {
            var packageTypes = await _packageTypeService.GetAllPackageTypesAsync(AppUser.BuildingId ?? 0);
            return Ok(packageTypes);
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IHttpActionResult> Add(PackageType packageType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (AppUser.BuildingId.HasValue)
            {
                packageType.BuildingId = AppUser.BuildingId.Value;
            }

            await _packageTypeService.AddAsync(packageType);

            return Ok(packageType);
        }

        [HttpPost]
        [Route("Update")]
        public async Task<IHttpActionResult> Update(PackageType packageType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _packageTypeService.UpdateAsync(packageType);

            return Ok();
        }

        [HttpPost]
        [Route("Remove")]
        public async Task<IHttpActionResult> Remove(PackageType packageType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _packageTypeService.RemoveAsync(packageType.Id);

            return Ok();
        }
    }
}