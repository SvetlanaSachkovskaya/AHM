using System.Collections.Generic;
using System.Threading.Tasks;
using AHM.Common.DomainModel;

namespace AHM.BusinessLayer.Interfaces
{
    public interface IJournalService
    {
        Task<ICollection<Event>> GetAllEventsAsync(int buildingId);

        Task AddAsync(Event eventEntity);

        Task UpdateAsync(Event eventEntity);
    }
}