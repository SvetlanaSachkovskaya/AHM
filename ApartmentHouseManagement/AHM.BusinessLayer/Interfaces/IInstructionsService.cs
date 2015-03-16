using System.Collections.Generic;
using System.Threading.Tasks;
using AHM.Common.DomainModel;

namespace AHM.BusinessLayer.Interfaces
{
    public interface IInstructionsService
    {
        Task<ICollection<Instruction>> GetAllInstructionsAsync(int buildingId);

        Task<ICollection<Instruction>> GetAllOpenInstructionsAsync(int buildingId);

        Task<Instruction> GetByIdAsync(int id);

        Task<ModifyDbStateResult> AddAsync(Instruction instruction);

        Task<ModifyDbStateResult> UpdateAsync(Instruction instruction);
    }
}