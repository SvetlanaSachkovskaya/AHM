using System.Collections.Generic;
using System.Threading.Tasks;
using AHM.BusinessLayer.Interfaces;
using AHM.Common.DomainModel;
using AHM.DataLayer.Interfaces;

namespace AHM.BusinessLayer.Services
{
    public class PackageTypeService : IPackageTypeService
    {
        private readonly IUnitOfWork _unitOfWork;


        public PackageTypeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<ICollection<PackageType>> GetAllPackageTypesAsync(int buildingId)
        {
            return await _unitOfWork.GetRepository<PackageType>().GetAllAsync(t => t.BuildingId == buildingId);
        }

        public async Task AddAsync(PackageType packageType)
        {
            _unitOfWork.GetRepository<PackageType>().Add(packageType);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(PackageType packageType)
        {
            _unitOfWork.GetRepository<PackageType>().Update(packageType);
            await _unitOfWork.SaveAsync();
        }

        public async Task RemoveAsync(int id)
        {
            _unitOfWork.GetRepository<PackageType>().Delete(id);
            await _unitOfWork.SaveAsync();
        }
    }
}