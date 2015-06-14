using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AHM.Common.DomainModel;

namespace AHM.DataLayer.Repositories
{
    public class UserRoleRepository: BaseRepository<UserRole>
    {
        public UserRoleRepository(AhmContext context)
            : base(context)
        {

        }

    }
}