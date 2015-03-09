using System;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using AHM.Common.DomainModel;
using AHM.DependencyInjection;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace AHM.WebAPI.Controllers
{
    public class BaseController : ApiController
    {
        private readonly ApplicationUserManager _appUserManager = null;


        protected ApplicationUserManager AppUserManager
        {
            get
            {
                return _appUserManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        protected User AppUser
        {
            get
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                if (claimsIdentity != null)
                {
                    var userIdClaim = claimsIdentity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Authentication);
                    if (userIdClaim != null)
                    {
                        int id;
                        Int32.TryParse(userIdClaim.Value, out id);
                        return AppUserManager.FindById(Convert.ToInt32(userIdClaim.Value));
                    }
                }

                return null;
            }
        }


        protected IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }
    }
}