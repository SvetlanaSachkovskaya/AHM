using System.Data.Entity;
using System.Threading.Tasks;
using AHM.Common.DomainModel;
using AHM.DataLayer.Interfaces;

namespace AHM.DataLayer.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(AhmContext context) : base(context)
        {

        }


        public async Task<User> GetByUsernameAsync(string username)
        {
            return await GetQuery(u => u.UserName == username).Include(u => u.Building).FirstOrDefaultAsync();
        }
    }
}