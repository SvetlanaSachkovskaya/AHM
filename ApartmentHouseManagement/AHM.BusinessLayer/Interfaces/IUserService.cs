using System.Collections.Generic;
using System.Threading.Tasks;
using AHM.Common.DomainModel;

namespace AHM.BusinessLayer.Interfaces
{
    public interface IUserService
    {
        Task<User> GetByUsernameAsync(string username);

        Task<ICollection<Role>> GetRolesAsync();

        Task<User> GetUserAsync(string username, string password);

        Task<UserModel> GetUserByIdAsync(int id);

        Task<IEnumerable<UserModel>> GetAllUsersAsync();

        Task<ModifyDbStateResult> AddUserAsync(User user, int roleId);

        Task<ModifyDbStateResult> UpdateUserAsync(UserModel userModel);
    }
}