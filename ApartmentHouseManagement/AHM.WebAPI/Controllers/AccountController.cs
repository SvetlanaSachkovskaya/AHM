using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using AHM.BusinessLayer.Interfaces;
using AHM.Common.DomainModel;
using AHM.WebAPI.Attributes;
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


        [Authorization(Roles = new[] { Roles.Admin })]
        [Route("RegisterUser")]
        public async Task<IHttpActionResult> RegisterUser(RegisterUserModel registerUserModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new User
            {
                UserName = registerUserModel.UserName,
                FirstName = registerUserModel.UserName,
                LastName = registerUserModel.LastName,
                Password = registerUserModel.Password,
                BuildingId = registerUserModel.BuildingId
            };

            var creationResult = await _userService.AddUserAsync(user, registerUserModel.RoleId);

            return creationResult.IsSuccessful ? (IHttpActionResult)Ok(user) : BadRequest(creationResult.Errors.First());
        }

        [Authorization(Roles = new[] { Roles.Admin })]
        [Route("UpdateUser")]
        public async Task<IHttpActionResult> UpdateUser(UserModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updateResult = await _userService.UpdateUserAsync(userModel);

            return updateResult.IsSuccessful ? (IHttpActionResult)Ok(userModel) : BadRequest(updateResult.Errors.First());
        }

        [Authorization(Roles = new[] { Roles.Admin })]
        [Route("GetRoles")]
        public async Task<IHttpActionResult> GetRoles()
        {
            var roles = await _userService.GetRolesAsync();

            return Ok(roles);
        }

        [HttpGet]
        [Route("GetByUsername")]
        public async Task<IHttpActionResult> GetByUsername(string username)
        {
            var user = await _userService.GetByUsernameAsync(username);
            var roles = await AppUserManager.GetRolesAsync(user.Id);
            var userModel = new AuthenticatedUserModel
            {
                BuildingName = user.Building != null ? user.Building.Name : String.Empty,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = roles.FirstOrDefault()
            };

            return Ok(userModel);
        }

        [HttpGet]
        [Route("GetUserById")]
        public async Task<IHttpActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);

            return Ok(user);
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<IHttpActionResult> GetAllUsers()
        {
            var user = await _userService.GetAllUsersAsync();

            return Ok(user.Select(u => new ShortUserModel(u)));
        }
    }
}