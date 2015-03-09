using System.Collections.Generic;
using System.Threading.Tasks;
using AHM.Common.DomainModel;

namespace AHM.BusinessLayer.Interfaces
{
    public interface IUtilitiesItemService
    {
        Task<ICollection<UtilitiesItem>> GetByBillIdAsync(int billId);
    }
}