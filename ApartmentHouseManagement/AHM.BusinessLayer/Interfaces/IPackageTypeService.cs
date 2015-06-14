using System.Collections.Generic;
using System.Threading.Tasks;
using AHM.Common.DomainModel;

namespace AHM.BusinessLayer.Interfaces
{
    public interface IPackageTypeService
    {
        Task<ICollection<PackageType>> GetAllPackageTypesAsync(int buildingId);

        Task<ModifyDbStateResult> AddAsync(PackageType packageType);

        Task<ModifyDbStateResult> UpdateAsync(PackageType packageType);

        Task<ModifyDbStateResult> RemoveAsync(int id);

        Task<bool> InUseAsync(int id);
    }
}