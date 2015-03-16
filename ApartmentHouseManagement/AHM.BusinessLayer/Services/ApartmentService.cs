using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AHM.BusinessLayer.Interfaces;
using AHM.Common.DomainModel;
using AHM.DataLayer.Interfaces;

namespace AHM.BusinessLayer.Services
{
    public class ApartmentService : BaseService, IApartmentService
    {
        public ApartmentService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            
        }


        public async Task<ICollection<Apartment>> GetAllApartmentsAsync(int buildingId)
        {
            return await UnitOfWork.GetRepository<Apartment>().GetAllAsync(a => a.BuildingId == buildingId);
        }

        public async Task<Apartment> GetApartmentByIdAsync(int id)
        {
            return await UnitOfWork.GetRepository<Apartment>().GetByIdAsync(id);
        }

        public async Task<ModifyDbStateResult> AddAsync(Apartment apartment)
        {
            var creationResult = await AddEntityAsync(apartment, "Failed to create Apartment", async () =>
            {
                apartment.Name = String.Format("{0}", apartment.Number);
                UnitOfWork.GetRepository<Apartment>().Add(apartment);
                await UnitOfWork.SaveAsync();
            });

            return creationResult;
        }

        public async Task<ModifyDbStateResult> UpdateAsync(Apartment apartment, int ownerId)
        {
            var updatingResult = await UpdateEntityAsync(apartment, "Failed to update Apartment", async () =>
            {
                var oldOwner =
                await
                    UnitOfWork.GetRepository<Occupant>()
                        .GetEntityAsync(o => o.ApartmentId == apartment.Id && o.IsOwner);
                if (oldOwner != null && oldOwner.Id != ownerId)
                {
                    oldOwner.IsOwner = false;
                    UnitOfWork.GetRepository<Occupant>().Update(oldOwner);
                }

                var owner = await UnitOfWork.GetRepository<Occupant>().GetByIdAsync(ownerId);
                if (owner != null)
                {
                    owner.IsOwner = true;
                    apartment.Name = String.Format("{0} - {1}", apartment.Number, owner.Name);

                    UnitOfWork.GetRepository<Occupant>().Update(owner);
                }
                else
                {
                    apartment.Name = String.Format("{0}", apartment.Number);
                }

                UnitOfWork.GetRepository<Apartment>().Update(apartment);
                await UnitOfWork.SaveAsync();
            });

            return updatingResult;
        }

        public async Task<ModifyDbStateResult> RemoveAsync(int id)
        {
            var result = await RemoveEntityAsync(id, "Failed to remove Apartment", async () =>
            {
                UnitOfWork.GetRepository<Apartment>().Delete(id);
                await UnitOfWork.SaveAsync();
            });

            return result;
        }
    }
}