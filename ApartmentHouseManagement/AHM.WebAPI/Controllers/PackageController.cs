﻿using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using AHM.BusinessLayer.Interfaces;
using AHM.Common.DomainModel;
using AHM.WebAPI.Models;

namespace AHM.WebAPI.Controllers
{
    [Authorize]
    [RoutePrefix("api/Post")]
    public class PackageController : BaseController
    {
        private readonly IPackageService _packageService;


        public PackageController(IPackageService packageService)
        {
            _packageService = packageService;
        }


        [HttpGet]
        [Route("GetAll")]
        public async Task<IHttpActionResult> GetAll()
        {
            var packages = await _packageService.GetAllPackagesAsync(AppUser.BuildingId ?? 0);
            return Ok(packages);
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IHttpActionResult> GetById(int id)
        {
            var package = await _packageService.GetByIdAsync(id);
            return Ok(package);
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IHttpActionResult> Add(CreatePackageModel packageModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var package = packageModel.GetPackage();
            package.OpenedByEmployeeId = AppUser.Id;

            var result = await _packageService.AddAsync(package);

            return result.IsSuccessful ? (IHttpActionResult)Ok(package) : BadRequest(result.Errors.First());
        }

        [HttpPost]
        [Route("Update")]
        public async Task<IHttpActionResult> Update(Package package)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            package.LastChangeDate = DateTime.Now;

            var result = await _packageService.UpdateAsync(package);

            return result.IsSuccessful ? (IHttpActionResult)Ok() : BadRequest(result.Errors.First());
        }
    }
}