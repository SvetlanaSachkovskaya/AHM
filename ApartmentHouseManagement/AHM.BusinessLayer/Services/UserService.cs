using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AHM.BusinessLayer.Interfaces;
using AHM.Common;
using AHM.Common.DomainModel;
using AHM.DataLayer.Interfaces;

namespace AHM.BusinessLayer.Services
{
    public class UserService : BaseService, IUserService
    {
        private readonly IUnitOfWork _unitOfWork;


        public UserService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _unitOfWork.UserRepository.GetByUsernameAsync(username);
        }

        public async Task<ICollection<Role>> GetRolesAsync()
        {
            return await _unitOfWork.GetRepository<Role>().GetAllAsync();
        }

        public async Task<User> GetUserAsync(string username, string password)
        {
            var user =
                await _unitOfWork.UserRepository.GetByUsernameAsync(username);

            return user != null && CipherMaker.EqualsPasswords(user.Password, password, user.Salt) ? user : null;
        }

        public async Task<UserModel> GetUserByIdAsync(int id)
        {
            var user = await _unitOfWork.UserRepository.GetUserByIdAsync(id);
            user.Password = CipherMaker.Decrypt(user.Password, user.Salt);
            return user;
        }

        public async Task<bool> UsernameExistsAsync(string username)
        {
            return await _unitOfWork.UserRepository.AnyAsync(u => u.UserName == username);
        }

        public async Task<IEnumerable<UserModel>> GetAllUsersAsync()
        {
            return await _unitOfWork.UserRepository.GetAllUsersAsync();
        }

        public async Task<ModifyDbStateResult> AddUserAsync(User user, int roleId)
        {
            user.Salt = Guid.NewGuid().ToString();
            user.Password = CipherMaker.Encrypt(user.Password, user.Salt);

            var creationResult = await AddEntityAsync(user, "Failed to create user", async () =>
            {
                _unitOfWork.UserRepository.Add(user);
                _unitOfWork.GetRepository<UserRole>().Add(new UserRole { RoleId = roleId, UserId = user.Id });
                await UnitOfWork.SaveAsync();
            });

            return creationResult;
        }

        public async Task<ModifyDbStateResult> UpdateUserAsync(UserModel userModel)
        {
            var user = await UnitOfWork.UserRepository.GetByIdAsync(userModel.Id);

            if (user.UserName != userModel.Username)
            {
                var usernameExists = await UsernameExistsAsync(userModel.Username);
                if (usernameExists)
                {
                    return new ModifyDbStateResult
                    {
                        IsSuccessful = false,
                        Errors = new List<string> { ValidationMessages.UsernameExists }
                    };
                }
            }

            user.FirstName = userModel.FirstName;
            user.LastName = userModel.LastName;
            user.UserName = userModel.Username;
            user.Password = CipherMaker.Encrypt(userModel.Password, user.Salt);
            user.BuildingId = userModel.BuildingId;
            user.LockoutEnabled = userModel.IsLocked;

            var updateResult = await UpdateEntityAsync(user, "Failed to update user", async () =>
            {
                _unitOfWork.UserRepository.Update(user);

                if (user.Roles.Any(ur => ur.RoleId != userModel.RoleId))
                {
                    user.Roles.Clear();
                    user.Roles.Add(new UserRole
                    {
                        UserId = userModel.Id,
                        RoleId = userModel.RoleId
                    });
                }

                await UnitOfWork.SaveAsync();
            });

            return updateResult;
        }

        public async Task<ModifyDbStateResult> ChangeUserLockStateAsync(int userId, bool isLocked)
        {
            var user = await UnitOfWork.UserRepository.GetByIdAsync(userId);
            user.LockoutEnabled = isLocked;

            var updateResult = await UpdateEntityAsync(user, "Failed to update user", async () =>
            {
                _unitOfWork.UserRepository.Update(user);

                await UnitOfWork.SaveAsync();
            });

            return updateResult;
        }
    }
}