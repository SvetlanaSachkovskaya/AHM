using System.Collections.Generic;
using System.Threading.Tasks;
using AHM.Common.DomainModel;

namespace AHM.BusinessLayer.Interfaces
{
    public interface IUtilitiesClauseService
    {
        Task<ICollection<UtilitiesClause>> GetAllUtilitiesClausesAsync(int buildingId);

        Task<ICollection<UtilitiesClause>> GetActiveUtilitiesClausesAsync(int buildingId);

        Task<UtilitiesClause> GetByIdAsync(int id);

        Task<ModifyDbStateResult> AddAsync(UtilitiesClause utilitiesClause);

        Task<ModifyDbStateResult> UpdateAsync(UtilitiesClause utilitiesClause);
    }
}