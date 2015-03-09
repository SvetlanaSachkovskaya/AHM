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
        public async Task<IHttpActionResult> Register(UserModel userModel)
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
                result = await AppUserManager.AddToRoleAsync(user.Id, Roles.Occupant.ToString());
            }

            var errorResult = GetErrorResult(result);

            return errorResult ?? Ok();
        }

        [HttpGet]
        [Route("GetByUsername")]
        public async Task<IHttpActionResult> GetByUsername(string username)
        {
            var result = await _userService.GetByUsernameAsync(username);
            
            return Ok(result);
        }
    }
}