using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AHM.Common;
using AHM.Common.DomainModel;
using AHM.DataLayer.Interfaces;

namespace AHM.DataLayer.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(AhmContext context)
            : base(context)
        {

        }


        public async Task<User> GetByUsernameAsync(string username)
        {
            return await GetQuery(u => u.UserName == username).Include(u => u.Building).FirstOrDefaultAsync();
        }

        public async Task<UserModel> GetUserByIdAsync(int id)
        {
            var user = await (from us in Context.Users
                               join userRole in Context.Set<UserRole>() on us.Id equals userRole.UserId
                               join role in Context.Roles on userRole.RoleId equals role.Id
                               where us.Id == id
                               select new UserModel
                               {
                                   Id = us.Id,
                                   FirstName = us.FirstName,
                                   LastName = us.LastName,
                                   Username = us.UserName,
                                   Password = us.Password,
                                   Salt = us.Salt,
                                   RoleName = role.Name,
                                   BuildingId = us.BuildingId,
                                   RoleId = role.Id,
                                   IsLocked = us.LockoutEnabled
                               }).FirstOrDefaultAsync();

            return user;
        }

        public async Task<ICollection<UserModel>> GetAllUsersAsync()
        {
            var users = await (from user in Context.Users
                join userRole in Context.Set<UserRole>() on user.Id equals userRole.UserId
                join role in Context.Roles on userRole.RoleId equals role.Id
                select new UserModel
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Username = user.UserName,
                    RoleName = role.Name,
                    BuildingId = user.BuildingId,
                    RoleId = role.Id,
                    IsLocked = user.LockoutEnabled
                }).ToListAsync();

            return users;
        }
    }
}