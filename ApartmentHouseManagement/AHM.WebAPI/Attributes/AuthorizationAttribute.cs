using System;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using AHM.Common.DomainModel;
using AHM.DependencyInjection;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace AHM.WebAPI.Attributes
{
    public class AuthorizationAttribute : AuthorizeAttribute
    {
        public new Roles[] Roles { get; set; }


        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            var identity = Thread.CurrentPrincipal.Identity;
            if (identity == null && HttpContext.Current != null)
            {
                identity = HttpContext.Current.User.Identity;
            }

            if (identity == null || !identity.IsAuthenticated)
            {
                return false;
            }

            var result = !Roles.Any();

            var claimsIdentity = identity as ClaimsIdentity;
            if (claimsIdentity != null)
            {
                var userIdClaim = claimsIdentity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Authentication);
                if (userIdClaim != null)
                {
                    int id;
                    Int32.TryParse(userIdClaim.Value, out id);

                    var userManager = actionContext.Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
                    if (Roles.Any(role => userManager.IsInRole(id, role.ToString())))
                    {
                        result = true;
                    }
                }
            }

            return result;
        }
    }
}