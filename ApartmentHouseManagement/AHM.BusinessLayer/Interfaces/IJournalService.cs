using System.Collections.Generic;
using System.Threading.Tasks;
using AHM.Common.DomainModel;

namespace AHM.BusinessLayer.Interfaces
{
    public interface IJournalService
    {
        Task<ICollection<Event>> GetAllActiveEventsAsync(int buildingId);

        Task<ICollection<Event>> GetEventsPerDay(int buildingId);

        Task<ICollection<Event>> GetEventsPerWeek(int buildingId);

        Task<ICollection<Event>> GetEventsPerMonth(int buildingId);

        Task<ICollection<Event>> GetEventsPerYear(int buildingId);

        Task<ModifyDbStateResult> AddAsync(Event eventEntity);

        Task<ModifyDbStateResult> UpdateAsync(Event eventEntity);
    }
}