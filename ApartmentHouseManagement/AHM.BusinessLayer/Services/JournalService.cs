using System.Collections.Generic;
using System.Threading.Tasks;
using AHM.Common.DomainModel;
using AHM.DataLayer.Interfaces;

namespace AHM.BusinessLayer.Services
{
    public class JournalService
    {
        private readonly IUnitOfWork _unitOfWork;


        public JournalService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<ICollection<Event>> GetAllEventsAsync(int buildingId)
        {
            return await _unitOfWork.GetRepository<Event>().GetAllAsync(e => e.BuildingId == buildingId && e.IsRemoved);
        }

        public async Task AddAsync(Event eventEntity)
        {
            _unitOfWork.GetRepository<Event>().Add(eventEntity);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(Event eventEntity)
        {
            _unitOfWork.GetRepository<Event>().Update(eventEntity);
            await _unitOfWork.SaveAsync();
        }
    }
}