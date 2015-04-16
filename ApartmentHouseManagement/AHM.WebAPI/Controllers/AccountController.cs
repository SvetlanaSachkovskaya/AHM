using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using AHM.BusinessLayer.Interfaces;
using AHM.Common.DomainModel;
using AHM.WebAPI.Models;

namespace AHM.WebAPI.Controllers
{
    [RoutePrefix("api/Account")]
    public class AccountController : BaseController
    {
        private readonly IUserService _userService;


        public AccountController(IUserService userService)
        {
            _userService = userService;
        }


        //todo: remove
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(UserLoginModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new User
            {
                UserName = userModel.UserName,
                Email = "test@test.com",
                EmailConfirmed = true,
                FirstName = userModel.UserName,
                LastName = "Test"
            };

            var result = await AppUserManager.CreateAsync(user, userModel.Password);

            if (result.Succeeded)
            {
                result = await AppUserManager.AddToRoleAsync(user.Id, Roles.Worker.ToString());
            }

            var errorResult = GetErrorResult(result);

            return errorResult ?? Ok();
        }

        [HttpGet]
        [Route("GetByUsername")]
        public async Task<IHttpActionResult> GetByUsername(string username)
        {
            var user = await _userService.GetByUsernameAsync(username);
            var roles = await AppUserManager.GetRolesAsync(user.Id);

            var userModel = new AuthenticatedUserModel()
            {
                BuildingName = user.Building.Name,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = roles.FirstOrDefault()
            };

            return Ok(userModel);
        }
    }
}