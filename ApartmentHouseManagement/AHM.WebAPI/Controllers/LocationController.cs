﻿using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using AHM.BusinessLayer.Interfaces;
using AHM.Common;
using AHM.Common.DomainModel;
using AHM.WebAPI.Attributes;

namespace AHM.WebAPI.Controllers
{
    [Authorization(Roles = new[] { Roles.Concierge })]
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
                return BadRequest(ModelState.SelectMany(m => m.Value.Errors).First().ErrorMessage);
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
                return BadRequest(ModelState.SelectMany(m => m.Value.Errors).First().ErrorMessage);
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
                return BadRequest(ModelState.SelectMany(m => m.Value.Errors).First().ErrorMessage);
            }

            var inUse = await _locationService.InUseAsync(location.Id);
            if (inUse)
            {
                return BadRequest(ValidationMessages.LocationInUse);
            }

            var result = await _locationService.RemoveAsync(location.Id);

            return result.IsSuccessful ? (IHttpActionResult)Ok(location) : BadRequest(result.Errors.First());
        }
    }
}