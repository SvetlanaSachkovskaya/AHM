using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AHM.Common.DomainModel;

namespace AHM.DataLayer.Repositories
{
    public class OccupantRepository : BaseRepository<Occupant>
    {
        public OccupantRepository(AhmContext context)
            : base(context)
        {

        }

        public async override Task<ICollection<Occupant>> GetAllAsync(Expression<Func<Occupant, bool>> filter = null)
        {
            return await GetQuery(filter, occupant => occupant.Apartment).ToListAsync();
        }
    }
}