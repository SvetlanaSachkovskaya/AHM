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
                return BadRequest(ModelState.SelectMany(m => m.Value.Errors).First().ErrorMessage);
            }

            if (AppUser.BuildingId.HasValue)
            {
                packageType.BuildingId = AppUser.BuildingId.Value;
            }

            var result = await _packageTypeService.AddAsync(packageType);

            return result.IsSuccessful ? (IHttpActionResult)Ok(packageType) : BadRequest(result.Errors.First());
        }

        [HttpPost]
        [Route("Update")]
        public async Task<IHttpActionResult> Update(PackageType packageType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.SelectMany(m => m.Value.Errors).First().ErrorMessage);
            }

            var result = await _packageTypeService.UpdateAsync(packageType);

            return result.IsSuccessful ? (IHttpActionResult)Ok(packageType) : BadRequest(result.Errors.First());
        }

        [HttpPost]
        [Route("Remove")]
        public async Task<IHttpActionResult> Remove(PackageType packageType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.SelectMany(m => m.Value.Errors).First().ErrorMessage);
            }

            var inUse = await _packageTypeService.InUseAsync(packageType.Id);
            if (inUse)
            {
                return BadRequest(ValidationMessages.PackageTypeInUse);
            }

            var result = await _packageTypeService.RemoveAsync(packageType.Id);

            return result.IsSuccessful ? (IHttpActionResult)Ok(packageType) : BadRequest(result.Errors.First());
        }
    }
}