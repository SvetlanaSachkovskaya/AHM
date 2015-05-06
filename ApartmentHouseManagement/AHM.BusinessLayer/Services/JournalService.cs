using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AHM.BusinessLayer.Interfaces;
using AHM.Common.DomainModel;
using AHM.DataLayer.Interfaces;

namespace AHM.BusinessLayer.Services
{
    public class JournalService : BaseService, IJournalService
    {
        public JournalService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }


        public async Task<ICollection<Event>> GetAllActiveEventsAsync(int buildingId)
        {
            return await UnitOfWork.GetRepository<Event>().GetAllAsync(e => e.BuildingId == buildingId && !e.IsRemoved);
        }

        public async Task<ICollection<Event>> GetEventsByDate(DateTime date, int buildingId)
        {
            var events = await
                UnitOfWork.GetRepository<Event>()
                    .GetAllAsync(e => e.BuildingId == buildingId && !e.IsRemoved);
            return events.Where(e => e.DateTime.Date == date.Date).OrderByDescending(e => e.DateTime).ToList();
        }

        public async Task<ModifyDbStateResult> AddAsync(Event eventEntity)
        {
            var creationResult = await AddEntityAsync(eventEntity, "Failed to create Event", async () =>
            {
                UnitOfWork.GetRepository<Event>().Add(eventEntity);
                await UnitOfWork.SaveAsync();
            });

            return creationResult;
        }

        public async Task<ModifyDbStateResult> UpdateAsync(Event eventEntity)
        {
            var updatingResult = await UpdateEntityAsync(eventEntity, "Failed to update Event", async () =>
            {
                UnitOfWork.GetRepository<Event>().Update(eventEntity);
                await UnitOfWork.SaveAsync();
            });

            return updatingResult; 
        }
    }
}