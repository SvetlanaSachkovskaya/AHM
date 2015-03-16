using System.Collections.Generic;
using System.Threading.Tasks;
using AHM.Common.DomainModel;

namespace AHM.BusinessLayer.Interfaces
{
    public interface ILocationService
    {
        Task<ICollection<Location>> GetAllLocationsAsync(int buildingId);

        Task<ModifyDbStateResult> AddAsync(Location location);

        Task<ModifyDbStateResult> UpdateAsync(Location location);

        Task<ModifyDbStateResult> RemoveAsync(int id);
    }
}