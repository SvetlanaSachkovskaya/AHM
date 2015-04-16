using AHM.Common.DomainModel;
using AHM.DataLayer;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace AHM.DependencyInjection
{
    public class ApplicationUserManager : UserManager<User, int>
    {
        public ApplicationUserManager(ApplicationUserStore store)
            : base(store)
        {

        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var appDbContext = context.Get<AhmContext>();
            var appUserManager = new ApplicationUserManager(new ApplicationUserStore(appDbContext));

            return appUserManager;
        }
    }
}