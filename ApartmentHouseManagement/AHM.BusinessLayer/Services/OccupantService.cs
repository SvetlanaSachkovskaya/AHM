using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AHM.BusinessLayer.Interfaces;
using AHM.Common.DomainModel;
using AHM.DataLayer.Interfaces;

namespace AHM.BusinessLayer.Services
{
    public class OccupantService : IOccupantService
    {
        private readonly IUnitOfWork _unitOfWork;


        public OccupantService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<ICollection<Occupant>> GetAllOccupantsAsync(int buildingId)
        {
            return
                await
                    _unitOfWork.GetRepository<Occupant>()
                        .GetAllAsync(o => o.Apartment.BuildingId == buildingId);
        }

        public async Task<Occupant> GetOccupantByIdAsync(int id)
        {
            return await _unitOfWork.GetRepository<Occupant>().GetByIdAsync(id);
        }

        public async Task<Occupant> GetApartmentOwnerAsync(int apartmentId)
        {
            return
                await
                    _unitOfWork.GetRepository<Occupant>().GetEntityAsync(o => o.ApartmentId == apartmentId && o.IsOwner);
        }

        public async Task<IEnumerable<Occupant>> GetOccupantsByApartmentIdAsync(int apartmentId)
        {
            return await _unitOfWork.GetRepository<Occupant>().GetAllAsync(o => o.ApartmentId == apartmentId);
        }

        public async Task AddAsync(Occupant occupant)
        {
            _unitOfWork.GetRepository<Occupant>().Add(occupant);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(Occupant occupant)
        {
            if (occupant.IsOwner)
            {
                var apartment = await _unitOfWork.GetRepository<Apartment>().GetByIdAsync(occupant.ApartmentId);
                if (apartment != null)
                {
                    apartment.Name = String.Format("{0} - {1}", apartment.Number, occupant.Name);
                }
                _unitOfWork.GetRepository<Apartment>().Update(apartment);
            }

            _unitOfWork.GetRepository<Occupant>().Update(occupant);
            await _unitOfWork.SaveAsync();
        }

        public async Task RemoveAsync(int id)
        {
            _unitOfWork.GetRepository<Occupant>().Delete(id);
            await _unitOfWork.SaveAsync();
        }
    }
}