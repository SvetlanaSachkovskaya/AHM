using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AHM.Common.DomainModel;

namespace AHM.DataLayer.Repositories
{
    public class UtilitiesItemRepository : BaseRepository<UtilitiesItem>
    {
        public UtilitiesItemRepository(AhmContext context) : base(context)
        {

        }


        public async override Task<ICollection<UtilitiesItem>> GetAllAsync(Expression<Func<UtilitiesItem, bool>> filter = null)
        {
            return await GetQuery(filter, item => item.UtilitiesClause).ToListAsync();
        }

        public override void Update(UtilitiesItem utilitiesItem)
        {
            AttachDependencies(utilitiesItem);
            base.Update(utilitiesItem);
        }


        private void AttachDependencies(UtilitiesItem utilitiesItem)
        {
            if (utilitiesItem.UtilitiesClause != null && utilitiesItem.UtilitiesClause.Id > 0)
            {
                utilitiesItem.UtilitiesClause = Context.Set<UtilitiesClause>().Find(utilitiesItem.UtilitiesClauseId);
                utilitiesItem.UtilitiesClauseId = utilitiesItem.UtilitiesClause.Id;
            }
            if (utilitiesItem.Bill != null && utilitiesItem.Bill.Id > 0)
            {
                utilitiesItem.Bill = Context.Set<Bill>().Find(utilitiesItem.Bill.Id);
                utilitiesItem.BillId = utilitiesItem.Bill.Id;
            }
        }
    }
}