using System.Collections.Generic;
using System.Threading.Tasks;
using AHM.BusinessLayer.Interfaces;
using AHM.Common.DomainModel;
using AHM.DataLayer.Interfaces;

namespace AHM.BusinessLayer.Services
{
    public class InstructionsService : IInstructionsService
    {
        private readonly IUnitOfWork _unitOfWork;


        public InstructionsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<ICollection<Instruction>> GetAllInstructionsAsync(int buildingId)
        {
            return await _unitOfWork.GetRepository<Instruction>().GetAllAsync(l => l.BuildingId == buildingId);
        }

        public async Task AddAsync(Instruction instruction)
        {
            _unitOfWork.GetRepository<Instruction>().Add(instruction);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(Instruction instruction)
        {
            _unitOfWork.GetRepository<Instruction>().Update(instruction);
            await _unitOfWork.SaveAsync();
        }
    }
}