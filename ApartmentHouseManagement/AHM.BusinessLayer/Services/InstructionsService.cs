using System;
using System.Collections.Generic;
using System.Linq;
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


        public async Task<ICollection<Instruction>> GetInstructionsByDateAsync(int buildingId, DateTime date, bool onlyOpen = false)
        {
            var instructions =
                await
                    UnitOfWork.GetRepository<Instruction>()
                        .GetAllAsync(l => l.BuildingId == buildingId && (!onlyOpen || !l.IsClosed));

            return instructions.Where(i => i.ExecutionDate.Date == date.Date).ToList();
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

        public async Task<ModifyDbStateResult> RemoveAsync(Instruction instruction)
        {
            var updatingResult = await UpdateEntityAsync(instruction, "Failed to remove Instruction", async () =>
            {
                UnitOfWork.GetRepository<Instruction>().Delete(instruction.Id);
                await UnitOfWork.SaveAsync();
            });

            return updatingResult;
        }
    }
}