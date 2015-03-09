using System.Collections.Generic;
using System.Threading.Tasks;
using AHM.BusinessLayer.Interfaces;
using AHM.Common.DomainModel;
using AHM.DataLayer.Interfaces;

namespace AHM.BusinessLayer.Services
{
    public class LocationService : ILocationService
    {
        private readonly IUnitOfWork _unitOfWork;


        public LocationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<ICollection<Location>> GetAllLocationsAsync(int buildingId)
        {
            return await _unitOfWork.GetRepository<Location>().GetAllAsync(l => l.BuildingId == buildingId);
        }

        public async Task AddAsync(Location location)
        {
            _unitOfWork.GetRepository<Location>().Add(location);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(Location location)
        {
            _unitOfWork.GetRepository<Location>().Update(location);
            await _unitOfWork.SaveAsync();
        }

        public async Task RemoveAsync(int id)
        {
            _unitOfWork.GetRepository<Location>().Delete(id);
            await _unitOfWork.SaveAsync();
        }
    }
}