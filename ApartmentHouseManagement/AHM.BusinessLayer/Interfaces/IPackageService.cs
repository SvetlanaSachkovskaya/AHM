using System.Collections.Generic;
using System.Threading.Tasks;
using AHM.Common.DomainModel;

namespace AHM.BusinessLayer.Interfaces
{
    public interface IPackageService
    {
        Task<ICollection<Package>> GetAllPackagesAsync(int buildingId);

        Task<ICollection<Package>> FilterPackagesAsync(int buildingId, int locationId, int packageTypeId);

        Task<Package> GetByIdAsync(int id);

        Task AddAsync(Package package);

        Task UpdateAsync(Package package);
    }
}