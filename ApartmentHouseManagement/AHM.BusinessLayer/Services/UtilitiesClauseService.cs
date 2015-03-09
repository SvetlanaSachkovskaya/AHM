using System.Collections.Generic;
using System.Threading.Tasks;
using AHM.BusinessLayer.Interfaces;
using AHM.Common.DomainModel;
using AHM.DataLayer.Interfaces;

namespace AHM.BusinessLayer.Services
{
    public class UtilitiesClauseService : IUtilitiesClauseService
    {
        private readonly IUnitOfWork _unitOfWork;


        public UtilitiesClauseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<ICollection<UtilitiesClause>> GetAllUtilitiesClausesAsync(int buildingId)
        {
            return await _unitOfWork.GetRepository<UtilitiesClause>().GetAllAsync(a => a.BuildingId == buildingId);
        }

        public async Task<ICollection<UtilitiesClause>> GetActiveUtilitiesClausesAsync(int buildingId)
        {
            return await _unitOfWork.GetRepository<UtilitiesClause>().GetAllAsync(a => a.BuildingId == buildingId && a.IsActive);
        }

        public async Task<UtilitiesClause> GetByIdAsync(int id)
        {
            return await _unitOfWork.GetRepository<UtilitiesClause>().GetEntityAsync(i => i.Id == id);
        }

        public async Task AddAsync(UtilitiesClause utilitiesClause)
        {
            _unitOfWork.GetRepository<UtilitiesClause>().Add(utilitiesClause);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(UtilitiesClause utilitiesClause)
        {
            _unitOfWork.GetRepository<UtilitiesClause>().Update(utilitiesClause);
            await _unitOfWork.SaveAsync();
        }
    }
}