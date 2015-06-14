using System.Collections.Generic;
using System.Threading.Tasks;
using AHM.BusinessLayer.Interfaces;
using AHM.Common.DomainModel;
using AHM.DataLayer.Interfaces;

namespace AHM.BusinessLayer.Services
{
    public class PackageTypeService : BaseService, IPackageTypeService
    {
        public PackageTypeService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }


        public async Task<ICollection<PackageType>> GetAllPackageTypesAsync(int buildingId)
        {
            return await UnitOfWork.GetRepository<PackageType>().GetAllAsync(t => t.BuildingId == buildingId);
        }

        public async Task<ModifyDbStateResult> AddAsync(PackageType packageType)
        {
            var creationResult = await AddEntityAsync(packageType, "Failed to create Package type", async () =>
            {
                UnitOfWork.GetRepository<PackageType>().Add(packageType);
                await UnitOfWork.SaveAsync();
            });

            return creationResult;
        }

        public async Task<ModifyDbStateResult> UpdateAsync(PackageType packageType)
        {
            var updatingResult = await UpdateEntityAsync(packageType, "Failed to update Package type", async () =>
            {
                UnitOfWork.GetRepository<PackageType>().Update(packageType);
                await UnitOfWork.SaveAsync();
            });

            return updatingResult;
        }

        public async Task<ModifyDbStateResult> RemoveAsync(int id)
        {
            var result = await RemoveEntityAsync(id, "Failed to remove Package type", async () =>
            {
                UnitOfWork.GetRepository<PackageType>().Delete(id);
                await UnitOfWork.SaveAsync();
            });

            return result;
        }

        public async Task<bool> InUseAsync(int id)
        {
            var inUse = await UnitOfWork.GetRepository<Package>().AnyAsync(p => p.PackageTypeId == id);
            return inUse;
        }
    }
}