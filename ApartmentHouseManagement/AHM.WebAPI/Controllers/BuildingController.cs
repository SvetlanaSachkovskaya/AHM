using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using AHM.BusinessLayer.Interfaces;
using AHM.Common.DomainModel;
using AHM.WebAPI.Attributes;

namespace AHM.WebAPI.Controllers
{
    [Authorization(Roles = new[] { Roles.Admin })]
    [Route("api/Building")]
    public class BuildingController : BaseController
    {
        private readonly IBuildingService _buildingService;


        public BuildingController(IBuildingService buildingService)
        {
            _buildingService = buildingService;
        }


        [HttpGet]
        [Route("GetAllBuildings")]
        public async Task<IHttpActionResult> GetAllBuildings()
        {
            var buildings = await _buildingService.GetAllBuildingsAsync();

            return Ok(buildings);
        }

        [HttpGet]
        [Route("GetBuildingById")]
        public async Task<IHttpActionResult> GetBuildingById(int id)
        {
            var building = await _buildingService.GetBuildingByIdAsync(id);

            return Ok(building);
        }
        
        [HttpPost]
        [Route("AddBuilding")]
        public async Task<IHttpActionResult> AddBuilding(Building building)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _buildingService.AddAsync(building);

            return result.IsSuccessful ? (IHttpActionResult)Ok(building) : BadRequest(result.Errors.First());
        }

        [HttpPost]
        [Route("UpdateBuilding")]
        public async Task<IHttpActionResult> UpdateBuilding(Building building)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _buildingService.UpdateAsync(building);

            return result.IsSuccessful ? (IHttpActionResult)Ok(building) : BadRequest(result.Errors.First());
        }
    }
}