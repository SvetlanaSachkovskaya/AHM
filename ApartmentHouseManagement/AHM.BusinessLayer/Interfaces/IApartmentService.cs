using System.Collections.Generic;
using System.Threading.Tasks;
using AHM.Common.DomainModel;

namespace AHM.BusinessLayer.Interfaces
{
    public interface IApartmentService
    {
        Task<ICollection<Apartment>> GetAllApartmentsAsync(int buildingId);

        Task<Apartment> GetApartmentByIdAsync(int id);

        Task AddAsync(Apartment apartment);

        Task UpdateAsync(Apartment apartment, int ownerId);

        Task RemoveAsync(int id);
    }
}