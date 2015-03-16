using System.Collections.Generic;
using System.Threading.Tasks;
using AHM.BusinessLayer.Interfaces;
using AHM.Common.DomainModel;
using AHM.DataLayer.Interfaces;

namespace AHM.BusinessLayer.Services
{
    public class PackageService : BaseService, IPackageService
    {
        public PackageService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

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
    }
}