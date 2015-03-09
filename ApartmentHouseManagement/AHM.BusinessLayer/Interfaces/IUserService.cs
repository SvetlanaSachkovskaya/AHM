using System.Threading.Tasks;
using AHM.Common.DomainModel;

namespace AHM.BusinessLayer.Interfaces
{
    public interface IUserService
    {
        Task<User> GetByUsernameAsync(string username);
    }
}