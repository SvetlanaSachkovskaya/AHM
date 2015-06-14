using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using AHM.BusinessLayer;
using AHM.BusinessLayer.Interfaces;
using AHM.Common;
using AHM.Common.DomainModel;
using AHM.WebAPI.Attributes;
using AHM.WebAPI.Models;

namespace AHM.WebAPI.Controllers
{
    [RoutePrefix("api/Building")]
    public class BuildingController : BaseController
    {
        private readonly IBuildingService _buildingService;


        public BuildingController(IBuildingService buildingService)
        {
            _buildingService = buildingService;
        }


        [HttpGet]
        [Route("GetAllBuildings")]
        [Authorization(Roles = new[] { Roles.Admin })]
        public async Task<IHttpActionResult> GetAllBuildings()
        {
            var buildings = await _buildingService.GetAllBuildingsAsync();

            return Ok(buildings);
        }

        [HttpGet]
        [Route("GetBuildingById")]
        [Authorization(Roles = new[] { Roles.Admin })]
        public async Task<IHttpActionResult> GetBuildingById(int id)
        {
            var building = await _buildingService.GetBuildingByIdAsync(id);

            return Ok(building);
        }

        [HttpGet]
        [Route("GetCurrentBuilding")]
        [Authorization(Roles = new[] { Roles.Accountant })]
        public async Task<IHttpActionResult> GetCurrentBuilding()
        {
            var building = AppUser.BuildingId.HasValue
                ? await _buildingService.GetBuildingByIdAsync(AppUser.BuildingId.Value)
                : null;

            return Ok(building);
        }

        [HttpPost]
        [Route("AddBuilding")]
        [Authorization(Roles = new[] { Roles.Admin })]
        public async Task<IHttpActionResult> AddBuilding(EditBuildingModel building)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.SelectMany(m => m.Value.Errors).First().ErrorMessage);
            }

            var result = await _buildingService.AddAsync(building.GetBuilding());

            return result.IsSuccessful ? (IHttpActionResult)Ok(building) : BadRequest(result.Errors.First());
        }

        [HttpPost]
        [Route("UpdateBuilding")]
        [Authorization(Roles = new[] { Roles.Admin })]
        public async Task<IHttpActionResult> UpdateBuilding(Building building)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.SelectMany(m => m.Value.Errors).First().ErrorMessage);
            }

            var result = await _buildingService.UpdateAsync(building);

            return result.IsSuccessful ? (IHttpActionResult)Ok(building) : BadRequest(result.Errors.First());
        }

        [HttpPost]
        [Route("UpdateUtilitiesSettings")]
        [Authorization(Roles = new[] { Roles.Accountant })]
        public async Task<IHttpActionResult> UpdateUtilitiesSettings(UtilitiesSettingsModel settings)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.SelectMany(m => m.Value.Errors).First().ErrorMessage);
            }

            var result = AppUser.BuildingId.HasValue
                ? await
                    _buildingService.UpdateUtilitiesSettingsAsync(settings.LastPayUtilitiesDay, settings.FinePercent,
                        AppUser.BuildingId.Value)
                : new ModifyDbStateResult
                {
                    IsSuccessful = false,
                    Errors = new List<string> { ValidationMessages.BuildingNotFound }
                };

            return result.IsSuccessful ? (IHttpActionResult)Ok() : BadRequest(result.Errors.First());
        }
    }
}