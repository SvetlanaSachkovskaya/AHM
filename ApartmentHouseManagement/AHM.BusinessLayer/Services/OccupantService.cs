using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AHM.BusinessLayer.Interfaces;
using AHM.Common.DomainModel;
using AHM.DataLayer.Interfaces;

namespace AHM.BusinessLayer.Services
{
    public class OccupantService : BaseService, IOccupantService
    {
        public OccupantService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }


        public async Task<ICollection<Occupant>> GetAllOccupantsAsync(int buildingId)
        {
            return
                await
                    UnitOfWork.GetRepository<Occupant>()
                        .GetAllAsync(o => o.Apartment.BuildingId == buildingId);
        }

        public async Task<Occupant> GetOccupantByIdAsync(int id)
        {
            return await UnitOfWork.GetRepository<Occupant>().GetByIdAsync(id);
        }

        public async Task<Occupant> GetApartmentOwnerAsync(int apartmentId)
        {
            return
                await
                    UnitOfWork.GetRepository<Occupant>().GetEntityAsync(o => o.ApartmentId == apartmentId && o.IsOwner);
        }

        public async Task<IEnumerable<Occupant>> GetOccupantsByApartmentIdAsync(int apartmentId)
        {
            return await UnitOfWork.GetRepository<Occupant>().GetAllAsync(o => o.ApartmentId == apartmentId && o.IsActive);
        }

        public async Task<ModifyDbStateResult> AddAsync(Occupant occupant)
        {
            var creationResult = await AddEntityAsync(occupant, "Failed to create Occupant", async () =>
            {
                UnitOfWork.GetRepository<Occupant>().Add(occupant);
                await UnitOfWork.SaveAsync();
            });

            return creationResult;
        }

        public async Task<ModifyDbStateResult> UpdateAsync(Occupant occupant)
        {
            var updatingResult = await UpdateEntityAsync(occupant, "Failed to update Occupant", async () =>
            {
                if (occupant.IsOwner)
                {
                    var apartment = await UnitOfWork.GetRepository<Apartment>().GetByIdAsync(occupant.ApartmentId);
                    if (apartment != null)
                    {
                        apartment.Name = String.Format("{0} - {1}", apartment.Number, occupant.Name);
                    }
                    UnitOfWork.GetRepository<Apartment>().Update(apartment);
                }

                UnitOfWork.GetRepository<Occupant>().Update(occupant);
                await UnitOfWork.SaveAsync();
            });

            return updatingResult;
        }

        public async Task<ModifyDbStateResult> RemoveAsync(int id)
        {
            var result = await RemoveEntityAsync(id, "Failed to remove Occupant", async () =>
            {
                UnitOfWork.GetRepository<Occupant>().Delete(id);
                await UnitOfWork.SaveAsync();
            });

            return result;
        }

        public async Task<bool> InUseAsync(int id)
        {
            var inUse = await UnitOfWork.GetRepository<NotificationOptions>().AnyAsync(p => p.OccupantId == id);
            return inUse;
        }
    }
}