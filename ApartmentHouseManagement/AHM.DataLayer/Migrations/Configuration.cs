using System;
using System.Data.Entity.Migrations;
using System.Linq;
using AHM.Common;
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
                context.SaveChanges();
            }

            if (!context.Users.Any())
            {
                var salt = Guid.NewGuid().ToString();
                var user = new User
                {
                    UserName = "admin",
                    FirstName = "John",
                    LastName = "Smith",
                    Salt = salt,
                    Password = CipherMaker.Encrypt("123456", salt)
                };

                context.Users.Add(user);
                context.SaveChanges();

                var admin = context.Roles.First(r => r.Name == Roles.Admin.ToString());
                context.Set<UserRole>().Add(new UserRole { RoleId = admin.Id, UserId = user.Id });
                context.SaveChanges();
            }
        }
    }
}
