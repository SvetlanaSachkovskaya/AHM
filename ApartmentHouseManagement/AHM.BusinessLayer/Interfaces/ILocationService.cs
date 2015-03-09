using System.Collections.Generic;
using System.Threading.Tasks;
using AHM.Common.DomainModel;

namespace AHM.BusinessLayer.Interfaces
{
    public interface ILocationService
    {
        Task<ICollection<Location>> GetAllLocationsAsync(int buildingId);

        Task AddAsync(Location location);

        Task UpdateAsync(Location location);

        Task RemoveAsync(int id);
    }
}