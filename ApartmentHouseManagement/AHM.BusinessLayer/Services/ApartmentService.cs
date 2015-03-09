using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AHM.BusinessLayer.Interfaces;
using AHM.Common.DomainModel;
using AHM.DataLayer.Interfaces;

namespace AHM.BusinessLayer.Services
{
    public class ApartmentService : IApartmentService
    {
        private readonly IUnitOfWork _unitOfWork;


        public ApartmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<ICollection<Apartment>> GetAllApartmentsAsync(int buildingId)
        {
            return await _unitOfWork.GetRepository<Apartment>().GetAllAsync(a => a.BuildingId == buildingId);
        }

        public async Task<Apartment> GetApartmentByIdAsync(int id)
        {
            return await _unitOfWork.GetRepository<Apartment>().GetByIdAsync(id);
        }

        public async Task AddAsync(Apartment apartment)
        {
            apartment.Name = String.Format("{0}", apartment.Number);
            _unitOfWork.GetRepository<Apartment>().Add(apartment);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(Apartment apartment, int ownerId)
        {
            var oldOwner =
                await
                    _unitOfWork.GetRepository<Occupant>()
                        .GetEntityAsync(o => o.ApartmentId == apartment.Id && o.IsOwner);
            if (oldOwner != null && oldOwner.Id != ownerId)
            {
                oldOwner.IsOwner = false;
                _unitOfWork.GetRepository<Occupant>().Update(oldOwner);
            }

            var owner = await _unitOfWork.GetRepository<Occupant>().GetByIdAsync(ownerId);
            if (owner != null)
            {
                owner.IsOwner = true;
                apartment.Name = String.Format("{0} - {1}", apartment.Number, owner.Name);

                _unitOfWork.GetRepository<Occupant>().Update(owner);
            }
            else
            {
                apartment.Name = String.Format("{0}", apartment.Number);
            }

            _unitOfWork.GetRepository<Apartment>().Update(apartment);
            await _unitOfWork.SaveAsync();
        }

        public async Task RemoveAsync(int id)
        {
            _unitOfWork.GetRepository<Apartment>().Delete(id);
            await _unitOfWork.SaveAsync();
        }
    }
}