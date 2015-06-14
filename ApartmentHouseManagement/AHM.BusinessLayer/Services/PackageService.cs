using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AHM.BusinessLayer.Interfaces;
using AHM.Common;
using AHM.Common.DomainModel;
using AHM.DataLayer.Interfaces;

namespace AHM.BusinessLayer.Services
{
    public class PackageService : BaseService, IPackageService
    {
        private readonly IEmailSender _emailSender;

        public PackageService(IUnitOfWork unitOfWork, IEmailSender emailSender) : base(unitOfWork)
        {
            _emailSender = emailSender;
        }


        public async Task<ICollection<Package>> GetAllPackagesAsync(int buildingId)
        {
            return await UnitOfWork.GetRepository<Package>().GetAllAsync(p => p.Apartment.BuildingId == buildingId && !p.IsClosed);
        }

        public async Task<ICollection<Package>> FilterPackagesAsync(int buildingId, int locationId, int packageTypeId)
        {
            return
                await
                    UnitOfWork.GetRepository<Package>()
                        .GetAllAsync(
                            p =>
                                p.Apartment.BuildingId == buildingId && !p.IsClosed &&
                                (p.LocationId == locationId || locationId <= 0) &&
                                (p.PackageTypeId == packageTypeId || packageTypeId <= 0));
        }

        public async Task<Package> GetByIdAsync(int id)
        {
            return await UnitOfWork.GetRepository<Package>().GetByIdAsync(id);
        }

        public async Task<ModifyDbStateResult> AddAsync(Package package)
        {
            var creationResult = await AddEntityAsync(package, "Failed to create Package", async () =>
            {
                UnitOfWork.GetRepository<Package>().Add(package);
                await UnitOfWork.SaveAsync();

                await NotifyOccupants(package.Id);
            });

            return creationResult;
        }

        public async Task<ModifyDbStateResult> UpdateAsync(Package package)
        {
            var updatingResult = await UpdateEntityAsync(package, "Failed to update Package", async () =>
            {
                UnitOfWork.GetRepository<Package>().Update(package);
                await UnitOfWork.SaveAsync();
            });

            return updatingResult;
        }

        private async Task NotifyOccupants(int pacakgeId)
        {
            var package = await UnitOfWork.GetRepository<Package>().GetByIdAsync(pacakgeId);

            if (package.NotificationOptions.ShouldNotifyAllOccupants)
            {
                var occupants =
                    await
                        UnitOfWork.GetRepository<Occupant>()
                            .GetAllAsync(o => o.ApartmentId == package.ApartmentId && o.IsActive);
                foreach (var occupant in occupants)
                {
                    if (!String.IsNullOrEmpty(occupant.Email))
                    {
                        _emailSender.Send(occupant.Email, Constants.PostEmailSubject,
                            String.Format(Constants.PostEmailMessage, package.PackageType.LongDescription));
                    }
                }
            }
            else if (package.NotificationOptions.Occupant != null)
            {
                if (!String.IsNullOrEmpty(package.NotificationOptions.Occupant.Email))
                {
                    _emailSender.Send(package.NotificationOptions.Occupant.Email, Constants.PostEmailSubject,
                        String.Format(Constants.PostEmailMessage, package.PackageType.LongDescription));
                }
            }
        }
    }
}