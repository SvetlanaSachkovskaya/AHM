using System.Collections.Generic;
using System.Threading.Tasks;
using AHM.Common;
using AHM.Common.DomainModel;

namespace AHM.BusinessLayer.Interfaces
{
    public interface IBillService
    {
        Task<ICollection<Bill>> GetAllBillsAsync(int buildingId, BillDateInteval dateInteval, bool? isPaid = null);

        Task<Bill> GetByIdAsync(int id);

        Task AddAsync(Bill bill, List<UtilitiesItem> utilitiesItems);

        Task UpdateAsync(Bill bill, List<UtilitiesItem> utilitiesItems);
    }
}