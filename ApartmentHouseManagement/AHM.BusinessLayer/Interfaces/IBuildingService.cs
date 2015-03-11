using System.Collections.Generic;
using System.Threading.Tasks;
using AHM.Common.DomainModel;

namespace AHM.BusinessLayer.Interfaces
{
    public interface IBuildingService
    {
        Task<ICollection<Building>> GetAllBuildingsAsync();

        Task<Building> GetBuildingByIdAsync(int id);

        Task AddAsync(Building building);

        Task UpdateAsync(Building building);

        Task RemoveAsync(int id);
    }
}