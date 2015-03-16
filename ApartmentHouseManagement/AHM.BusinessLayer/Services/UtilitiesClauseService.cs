using System.Collections.Generic;
using System.Threading.Tasks;
using AHM.BusinessLayer.Interfaces;
using AHM.Common.DomainModel;
using AHM.DataLayer.Interfaces;

namespace AHM.BusinessLayer.Services
{
    public class UtilitiesClauseService : BaseService, IUtilitiesClauseService
    {
        public UtilitiesClauseService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }


        public async Task<ICollection<UtilitiesClause>> GetAllUtilitiesClausesAsync(int buildingId)
        {
            return await UnitOfWork.GetRepository<UtilitiesClause>().GetAllAsync(a => a.BuildingId == buildingId);
        }

        public async Task<ICollection<UtilitiesClause>> GetActiveUtilitiesClausesAsync(int buildingId)
        {
            return await UnitOfWork.GetRepository<UtilitiesClause>().GetAllAsync(a => a.BuildingId == buildingId && a.IsActive);
        }

        public async Task<UtilitiesClause> GetByIdAsync(int id)
        {
            return await UnitOfWork.GetRepository<UtilitiesClause>().GetEntityAsync(i => i.Id == id);
        }

        public async Task<ModifyDbStateResult> AddAsync(UtilitiesClause utilitiesClause)
        {
            var creationResult = await AddEntityAsync(utilitiesClause, "Failed to create Utilities clause", async () =>
            {
                UnitOfWork.GetRepository<UtilitiesClause>().Add(utilitiesClause);
                await UnitOfWork.SaveAsync();
            });

            return creationResult;
        }

        public async Task<ModifyDbStateResult> UpdateAsync(UtilitiesClause utilitiesClause)
        {
            var updatingResult = await UpdateEntityAsync(utilitiesClause, "Failed to update Utilities clause", async () =>
            {
                UnitOfWork.GetRepository<UtilitiesClause>().Update(utilitiesClause);
                await UnitOfWork.SaveAsync();
            });

            return updatingResult;
        }
    }
}