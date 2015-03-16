using System.Collections.Generic;
using System.Threading.Tasks;
using AHM.BusinessLayer.Interfaces;
using AHM.Common.DomainModel;
using AHM.DataLayer.Interfaces;

namespace AHM.BusinessLayer.Services
{
    public class InstructionsService : BaseService, IInstructionsService
    {
        public InstructionsService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }


        public async Task<ICollection<Instruction>> GetAllInstructionsAsync(int buildingId)
        {
            return await UnitOfWork.GetRepository<Instruction>().GetAllAsync(l => l.BuildingId == buildingId);
        }

        public async Task<ICollection<Instruction>> GetAllOpenInstructionsAsync(int buildingId)
        {
            return await UnitOfWork.GetRepository<Instruction>().GetAllAsync(i => i.BuildingId == buildingId && !i.IsClosed );
        }

        public async Task<Instruction> GetByIdAsync(int id)
        {
            return await UnitOfWork.GetRepository<Instruction>().GetByIdAsync(id);
        }

        public async Task<ModifyDbStateResult> AddAsync(Instruction instruction)
        {
            var creationResult = await AddEntityAsync(instruction, "Failed to create Instruction", async () =>
            {
                UnitOfWork.GetRepository<Instruction>().Add(instruction);
                await UnitOfWork.SaveAsync();
            });

            return creationResult;
        }

        public async Task<ModifyDbStateResult> UpdateAsync(Instruction instruction)
        {
            var updatingResult = await UpdateEntityAsync(instruction, "Failed to update Instruction", async () =>
            {
                UnitOfWork.GetRepository<Instruction>().Update(instruction);
                await UnitOfWork.SaveAsync();
            });

            return updatingResult;
        }
    }
}