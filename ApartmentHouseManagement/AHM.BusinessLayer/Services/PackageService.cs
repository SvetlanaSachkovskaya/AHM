using System.Collections.Generic;
using System.Threading.Tasks;
using AHM.BusinessLayer.Interfaces;
using AHM.Common.DomainModel;
using AHM.DataLayer.Interfaces;

namespace AHM.BusinessLayer.Services
{
    public class PackageService : IPackageService
    {
        private readonly IUnitOfWork _unitOfWork;


        public PackageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<ICollection<Package>> GetAllPackagesAsync(int buildingId)
        {
            return await _unitOfWork.GetRepository<Package>().GetAllAsync(p => p.Apartment.BuildingId == buildingId && !p.IsClosed);
        }

        public async Task<ICollection<Package>> FilterPackagesAsync(int buildingId, int locationId, int packageTypeId)
        {
            return
                await
                    _unitOfWork.GetRepository<Package>()
                        .GetAllAsync(
                            p =>
                                p.Apartment.BuildingId == buildingId && !p.IsClosed &&
                                (p.LocationId == locationId || locationId <= 0) &&
                                (p.PackageTypeId == packageTypeId || packageTypeId <= 0));
        }

        public async Task<Package> GetByIdAsync(int id)
        {
            return await _unitOfWork.GetRepository<Package>().GetByIdAsync(id);
        }

        public async Task AddAsync(Package package)
        {
            _unitOfWork.GetRepository<Package>().Add(package);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(Package package)
        {
            _unitOfWork.GetRepository<Package>().Update(package);
            await _unitOfWork.SaveAsync();
        }
    }
}