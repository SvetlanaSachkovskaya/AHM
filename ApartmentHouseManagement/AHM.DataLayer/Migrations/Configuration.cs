using System.Data.Entity.Migrations;
using System.Linq;
using AHM.Common.DomainModel;

namespace AHM.DataLayer.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<AhmContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AhmContext context)
        {
            if (!context.Roles.Any())
            {
                context.Roles.Add(new Role { Name = Roles.Admin.ToString() });
                context.Roles.Add(new Role { Name = Roles.Concierge.ToString() });
                context.Roles.Add(new Role { Name = Roles.Manager.ToString() });
                context.Roles.Add(new Role { Name = Roles.Worker.ToString() });
                context.Roles.Add(new Role { Name = Roles.Accountant.ToString() });
            }

            context.SaveChanges();

            //var manager = new ApplicationUserManager(new ApplicationUserStore(new AhmContext()));

            var user = new User
            {
                UserName = "SuperPowerUser",
                Email = "taiseer.joudeh@mymail.com",
                EmailConfirmed = true,
                FirstName = "Taiseer",
                LastName = "Joudeh"
            };

            //manager.Create(user, "MySuperP@ssword!");
            //manager.AddToRole(user.Id, Roles.Admin.ToString());
        }
    }
}
