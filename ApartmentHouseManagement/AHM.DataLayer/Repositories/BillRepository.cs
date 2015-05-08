using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AHM.Common.DomainModel;
using AHM.DataLayer.Interfaces;

namespace AHM.DataLayer.Repositories
{
    public class BillRepository : BaseRepository<Bill>, IBillRepository
    {
        public BillRepository(AhmContext context) : base(context)
        {

        }


        public async override Task<ICollection<Bill>> GetAllAsync(Expression<Func<Bill, bool>> filter = null)
        {
            return await GetQuery(filter, bill => bill.Apartment).ToListAsync();
        }

        public async override Task<Bill> GetByIdAsync(int id)
        {
            return await GetQuery(b => b.Id == id, bill => bill.Apartment, bill => bill.Apartment.Building).FirstOrDefaultAsync();
        }

        public async Task<Bill> GetLastBillAsync(int apartmentId)
        {
            return await GetQuery(b => b.ApartmentId == apartmentId, bill => bill.Apartment.Building).OrderByDescending(b => b.Date).FirstOrDefaultAsync();
        }

        public override void Update(Bill bill)
        {
            AttachDependencies(bill);
            base.Update(bill);
        }

        public void DeleteOldUtilitiesItems(IEnumerable<UtilitiesItem> newUtilitiesItems, int billId)
        {
            var utilitiesItemsBeforeUpdate = Context.UtilitiesItems.Where(i => i.BillId == billId).ToList();
            var removedUtilitiesItems =
                utilitiesItemsBeforeUpdate.Where(i => newUtilitiesItems.FirstOrDefault(it => it.Id == i.Id) == null);

            foreach (var removedUtilitiesItem in removedUtilitiesItems)
            {
                Delete(removedUtilitiesItem.Id);
            }
        }

        private void AttachDependencies(Bill bill)
        {
            if (bill.ApartmentId > 0)
            {
                bill.Apartment = Context.Set<Apartment>().Find(bill.ApartmentId);
            }
        }
    }
}