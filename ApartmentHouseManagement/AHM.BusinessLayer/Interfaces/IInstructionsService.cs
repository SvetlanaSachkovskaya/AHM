using System.Collections.Generic;
using System.Threading.Tasks;
using AHM.Common.DomainModel;

namespace AHM.BusinessLayer.Interfaces
{
    public interface IInstructionsService
    {
        Task<ICollection<Instruction>> GetAllInstructionsAsync(int buildingId);

        Task AddAsync(Instruction instruction);

        Task UpdateAsync(Instruction instruction);
    }
}