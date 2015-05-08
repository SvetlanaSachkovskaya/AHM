using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AHM.Common.DomainModel;

namespace AHM.BusinessLayer.Interfaces
{
    public interface IInstructionsService
    {
        Task<ICollection<Instruction>> GetInstructionsByDateAsync(int buildingId, DateTime date, bool onlyOpen);

        Task<Instruction> GetByIdAsync(int id);

        Task<ModifyDbStateResult> AddAsync(Instruction instruction);

        Task<ModifyDbStateResult> UpdateAsync(Instruction instruction);

        Task<ModifyDbStateResult> RemoveAsync(Instruction instruction);
    }
}