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

        public async Task<ICollection<Event>> GetEventsPerDay(int buildingId)
        {
            var events = await
                UnitOfWork.GetRepository<Event>()
                    .GetAllAsync(e => e.BuildingId == buildingId && !e.IsRemoved);
            return events.Where(e => e.DateTime.Date == DateTime.Now.Date).ToList();
        }

        public async Task<ICollection<Event>> GetEventsPerWeek(int buildingId)
        {
            var events = await
                UnitOfWork.GetRepository<Event>()
                    .GetAllAsync(e => e.BuildingId == buildingId && !e.IsRemoved);
            return events.Where(e => e.DateTime.Date <= DateTime.Now && e.DateTime.Date > DateTime.Now.AddDays(-7)).ToList();
        }

        public async Task<ICollection<Event>> GetEventsPerMonth(int buildingId)
        {
            return
                await
                    UnitOfWork.GetRepository<Event>()
                        .GetAllAsync(e => e.BuildingId == buildingId && !e.IsRemoved && e.DateTime.Month == DateTime.Now.Month);
        }

        public async Task<ICollection<Event>> GetEventsPerYear(int buildingId)
        {
            return
                await
                    UnitOfWork.GetRepository<Event>()
                        .GetAllAsync(e => e.BuildingId == buildingId && !e.IsRemoved && e.DateTime.Year == DateTime.Now.Year);
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