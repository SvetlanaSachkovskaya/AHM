using System.Collections.Generic;
using System.Threading.Tasks;
using AHM.BusinessLayer.Interfaces;
using AHM.Common.DomainModel;
using AHM.DataLayer.Interfaces;

namespace AHM.BusinessLayer.Services
{
    public class BuildingService : BaseService, IBuildingService
    {
        public BuildingService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }


        public async Task<ICollection<Building>> GetAllBuildingsAsync()
        {
            return await UnitOfWork.GetRepository<Building>().GetAllAsync();
        }

        public async Task<Building> GetBuildingByIdAsync(int id)
        {
            return await UnitOfWork.GetRepository<Building>().GetByIdAsync(id);
        }

        public async Task<ModifyDbStateResult> AddAsync(Building building)
        {
            var creationResult = await AddEntityAsync(building, "Failed to create Building", async () =>
            {
                UnitOfWork.GetRepository<Building>().Add(building);
                await UnitOfWork.SaveAsync();
            });

            return creationResult;
        }

        public async Task<ModifyDbStateResult> UpdateAsync(Building building)
        {
            var updatingResult = await UpdateEntityAsync(building, "Failed to update Building", async () =>
            {
                UnitOfWork.GetRepository<Building>().Update(building);
                await UnitOfWork.SaveAsync();
            });

            return updatingResult;
        }

        public async Task<ModifyDbStateResult> RemoveAsync(int id)
        {
            var result = await RemoveEntityAsync(id, "Failed to remove Building", async () =>
            {
                UnitOfWork.GetRepository<Building>().Delete(id);
                await UnitOfWork.SaveAsync();
            });

            return result;
        }
    }
}