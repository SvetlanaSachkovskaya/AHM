using System.Collections.Generic;
using System.Threading.Tasks;
using AHM.Common.DomainModel;

namespace AHM.BusinessLayer.Interfaces
{
    public interface IUserService
    {
        Task<User> GetByUsernameAsync(string username);

        Task<ICollection<Role>> GetRolesAsync();

        Task<ModifyDbStateResult> AddUserAsync(User user, int roleId);

        Task<ModifyDbStateResult> UpdateUserAsync(User user, int roleId);
    }
}