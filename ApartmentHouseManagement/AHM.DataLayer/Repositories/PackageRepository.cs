using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AHM.Common.DomainModel;
using AHM.DataLayer.Interfaces;

namespace AHM.DataLayer.Repositories
{
    public class PackageRepository : BaseRepository<Package>, IPackageRepository
    {
        public PackageRepository(AhmContext context)
            : base(context)
        {

        }

        public async override Task<ICollection<Package>> GetAllAsync(Expression<Func<Package, bool>> filter = null)
        {
            return await
                GetQuery(filter,
                package => package.Apartment,
                package => package.Location,
                package => package.PackageType,
                package => package.NotificationOptions,
                package => package.NotificationOptions.Occupant)
                    .ToListAsync();
        }

        public async override Task<Package> GetByIdAsync(int id)
        {
            return await GetQuery(p => p.Id == id,
                package => package.Apartment,
                package => package.PackageType,
                package => package.NotificationOptions,
                package => package.NotificationOptions.Occupant).FirstOrDefaultAsync();
        }

        public override void Update(Package package)
        {
            AttachDependencies(package);
            base.Update(package);
        }


        private void AttachDependencies(Package package)
        {
            if (package.OpenedByEmployee != null && package.OpenedByEmployee.Id > 0)
            {
                package.OpenedByEmployee = Context.Set<User>().Find(package.OpenedByEmployee.Id);
                package.OpenedByEmployeeId = package.OpenedByEmployee.Id;
            }
            if (package.Location != null && package.Location.Id > 0)
            {
                package.Location = Context.Set<Location>().Find(package.Location.Id);
                package.LocationId = package.Location.Id;
            }
            if (package.Apartment != null && package.Apartment.Id > 0)
            {
                package.Apartment = Context.Set<Apartment>().Find(package.Apartment.Id);
                package.ApartmentId = package.Apartment.Id;
            }
            if (package.NotificationOptions.Occupant != null && package.NotificationOptions.Occupant.Id > 0)
            {
                package.NotificationOptions.Occupant = Context.Set<Occupant>().Find(package.NotificationOptions.Occupant.Id);
                package.NotificationOptions.OccupantId = package.NotificationOptions.Occupant.Id;
            }
            if (package.PackageType != null && package.PackageType.Id > 0)
            {
                package.PackageType = Context.Set<PackageType>().Find(package.PackageType.Id);
                package.PackageTypeId = package.PackageType.Id;
            }
        }
    }
}