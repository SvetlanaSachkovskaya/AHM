using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AHM.Common.DomainModel;

namespace AHM.BusinessLayer.Interfaces
{
    public interface IBillService
    {
        Task<ICollection<Bill>> GetAllBillsAsync(int buildingId, bool onlyOpen = false);

        Task<ICollection<Bill>> GetBillsByDateAsync(int buildingId, DateTime date, bool onlyOpen = false);

        Task<Bill> GetByIdAsync(int id);

        Task<ModifyDbStateResult> AddAsync(Bill bill, List<UtilitiesItem> utilitiesItems);

        Task<ModifyDbStateResult> UpdateAsync(Bill bill, List<UtilitiesItem> utilitiesItems = null);

        Task<ModifyDbStateResult> SendEmailAsync(Bill bill, string email, string username, string password,
            string filePath);

        Task<ModifyDbStateResult> PayBillAsync(Bill bill);
    }
}