using System.Threading.Tasks;
using AHM.Common.DomainModel;

namespace AHM.DataLayer.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetByUsernameAsync(string username);
    }
}