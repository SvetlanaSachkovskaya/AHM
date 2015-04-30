using System;
using System.Collections.Generic;
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


        public UserService(IUnitOfWork unitOfWork) : base(unitOfWork)
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
                _unitOfWork.GetRepository<UserRole>().Add(new UserRole {RoleId = roleId, UserId = user.Id});
                await UnitOfWork.SaveAsync();
            });

            return creationResult;
        }

        public async Task<ModifyDbStateResult> UpdateUserAsync(UserModel userModel)
        {
            var user = await UnitOfWork.UserRepository.GetByIdAsync(userModel.Id);

            user.FirstName = userModel.FirstName;
            user.LastName = userModel.LastName;
            user.UserName = userModel.Username;
            user.Password = CipherMaker.Encrypt(userModel.Password, user.Salt);
            user.BuildingId = userModel.BuildingId;
            user.LockoutEnabled = userModel.IsLocked;

            var updateResult = await UpdateEntityAsync(user, "Failed to update user", async () =>
            {
                _unitOfWork.UserRepository.Update(user);

                var userRole =
                    await _unitOfWork.GetRepository<UserRole>().GetEntityAsync(ur => ur.UserId == userModel.Id);
                if (userModel.RoleId != userRole.RoleId)
                {
                    userRole.RoleId = userModel.RoleId;
                    _unitOfWork.GetRepository<UserRole>().Update(userRole);
                }
                
                await UnitOfWork.SaveAsync();
            });

            return updateResult;
        }
    }
}