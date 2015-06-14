using System.Collections.Generic;
using System.Threading.Tasks;
using AHM.Common.DomainModel;

namespace AHM.BusinessLayer.Interfaces
{
    public interface IOccupantService
    {
        Task<ICollection<Occupant>> GetAllOccupantsAsync(int buildingId);

        Task<IEnumerable<Occupant>> GetOccupantsByApartmentIdAsync(int apartmentId);

        Task<Occupant> GetOccupantByIdAsync(int id);

        Task<Occupant> GetApartmentOwnerAsync(int apartmentId);

        Task<ModifyDbStateResult> AddAsync(Occupant occupant);

        Task<ModifyDbStateResult> UpdateAsync(Occupant occupant);

        Task<ModifyDbStateResult> RemoveAsync(int id);

        Task<bool> InUseAsync(int id);
    }
}