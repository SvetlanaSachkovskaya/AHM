using System.Data.Entity;
using AHM.Common.DomainModel;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AHM.DependencyInjection
{
    public class ApplicationRoleStore : RoleStore<Role, int, UserRole>
    {
        public ApplicationRoleStore(DbContext context) : base(context)
        {

        }
    }
}