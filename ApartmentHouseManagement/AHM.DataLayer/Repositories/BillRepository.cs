using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AHM.Common.DomainModel;

namespace AHM.DataLayer.Repositories
{
    public class BillRepository : BaseRepository<Bill>
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
    }
}