using System.Collections.Generic;
using System.Threading.Tasks;
using AHM.Common.DomainModel;

namespace AHM.BusinessLayer.Interfaces
{
    public interface IBuildingService
    {
        Task<ICollection<Building>> GetAllBuildingsAsync();

        Task<Building> GetBuildingByIdAsync(int id);

        Task<ModifyDbStateResult> AddAsync(Building building);

        Task<ModifyDbStateResult> UpdateAsync(Building building);

        Task<ModifyDbStateResult> RemoveAsync(int id);

        Task<ModifyDbStateResult> UpdateUtilitiesSettingsAsync(int lastPayDay, double finePercent, int buidingId);
    }
}