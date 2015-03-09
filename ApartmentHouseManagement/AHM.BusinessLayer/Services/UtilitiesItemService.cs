using System.Collections.Generic;
using System.Threading.Tasks;
using AHM.BusinessLayer.Interfaces;
using AHM.Common.DomainModel;
using AHM.DataLayer.Interfaces;

namespace AHM.BusinessLayer.Services
{
    public class UtilitiesItemService : IUtilitiesItemService
    {
        private readonly IUnitOfWork _unitOfWork;


        public UtilitiesItemService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<ICollection<UtilitiesItem>> GetByBillIdAsync(int billId)
        {
            return await _unitOfWork.GetRepository<UtilitiesItem>().GetAllAsync(i => i.BillId == billId);
        }
    }
}