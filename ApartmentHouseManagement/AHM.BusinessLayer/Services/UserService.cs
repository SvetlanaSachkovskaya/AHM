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

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _unitOfWork.UserRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _unitOfWork.UserRepository.GetAllAsync();
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

        public async Task<ModifyDbStateResult> UpdateUserAsync(User user, int roleId)
        {
            user.Salt = Guid.NewGuid().ToString();
            user.Password = CipherMaker.Encrypt(user.Password, user.Salt);

            var updateResult = await UpdateEntityAsync(user, "Failed to update user", async () =>
            {
                _unitOfWork.UserRepository.Update(user);
                _unitOfWork.GetRepository<UserRole>().Add(new UserRole { RoleId = roleId, UserId = user.Id });
                await UnitOfWork.SaveAsync();
            });

            return updateResult;
        }
    }
}