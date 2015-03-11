using System.Collections.Generic;
using System.Threading.Tasks;
using AHM.BusinessLayer.Interfaces;
using AHM.Common.DomainModel;
using AHM.DataLayer.Interfaces;

namespace AHM.BusinessLayer.Services
{
    public class BuildingService : IBuildingService
    {
        private readonly IUnitOfWork _unitOfWork;


        public BuildingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<ICollection<Building>> GetAllBuildingsAsync()
        {
            return await _unitOfWork.GetRepository<Building>().GetAllAsync();
        }

        public async Task<Building> GetBuildingByIdAsync(int id)
        {
            return await _unitOfWork.GetRepository<Building>().GetByIdAsync(id);
        }

        public async Task AddAsync(Building building)
        {
            _unitOfWork.GetRepository<Building>().Add(building);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(Building building)
        {
            _unitOfWork.GetRepository<Building>().Update(building);
            await _unitOfWork.SaveAsync();
        }

        public async Task RemoveAsync(int id)
        {
            _unitOfWork.GetRepository<Building>().Delete(id);
            await _unitOfWork.SaveAsync();
        }
    }
}