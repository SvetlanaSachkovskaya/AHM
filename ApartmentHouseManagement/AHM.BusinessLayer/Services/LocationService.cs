using System.Collections.Generic;
using System.Threading.Tasks;
using AHM.BusinessLayer.Interfaces;
using AHM.Common.DomainModel;
using AHM.DataLayer.Interfaces;

namespace AHM.BusinessLayer.Services
{
    public class LocationService : BaseService, ILocationService
    {
        public LocationService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            
        }


        public async Task<ICollection<Location>> GetAllLocationsAsync(int buildingId)
        {
            return await UnitOfWork.GetRepository<Location>().GetAllAsync(l => l.BuildingId == buildingId);
        }

        public async Task<ModifyDbStateResult> AddAsync(Location location)
        {
            var creationResult = await AddEntityAsync(location, "Failed to create Location", async () =>
            {
                UnitOfWork.GetRepository<Location>().Add(location);
                await UnitOfWork.SaveAsync();
            });

            return creationResult;
        }

        public async Task<ModifyDbStateResult> UpdateAsync(Location location)
        {
            var updatingResult = await UpdateEntityAsync(location, "Failed to update Location", async () =>
            {
                UnitOfWork.GetRepository<Location>().Update(location);
                await UnitOfWork.SaveAsync();
            });

            return updatingResult;
        }

        public async Task<ModifyDbStateResult> RemoveAsync(int id)
        {
            var result = await RemoveEntityAsync(id, "Failed to remove Location", async () =>
            {
                UnitOfWork.GetRepository<Location>().Delete(id);
                await UnitOfWork.SaveAsync();
            });

            return result;
        }

        public async Task<bool> InUseAsync(int id)
        {
            var inUse = await UnitOfWork.GetRepository<Package>().AnyAsync(p => p.LocationId == id);
            return inUse;
        }
    }
}