using System.Collections.Generic;
using System.Threading.Tasks;
using AHM.Common.DomainModel;

namespace AHM.BusinessLayer.Interfaces
{
    public interface IPackageTypeService
    {
        Task<ICollection<PackageType>> GetAllPackageTypesAsync(int buildingId);

        Task AddAsync(PackageType packageType);

        Task UpdateAsync(PackageType packageType);

        Task RemoveAsync(int id);
    }
}