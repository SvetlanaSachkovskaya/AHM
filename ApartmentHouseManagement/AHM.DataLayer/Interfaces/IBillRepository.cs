using System.Threading.Tasks;
using AHM.Common.DomainModel;

namespace AHM.DataLayer.Interfaces
{
    public interface IBillRepository : IBaseRepository<Bill>
    {
        Task<Bill> GetLastBillAsync(int apartmentId);
    }
}