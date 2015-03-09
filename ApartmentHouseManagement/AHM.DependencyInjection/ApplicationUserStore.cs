using System.Data.Entity;
using AHM.Common.DomainModel;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AHM.DependencyInjection
{
    public class ApplicationUserStore : UserStore<User, Role, int, UserLogin, UserRole, UserClaim>
    {
        public ApplicationUserStore(DbContext context) : base(context)
        {

        }
    }
}