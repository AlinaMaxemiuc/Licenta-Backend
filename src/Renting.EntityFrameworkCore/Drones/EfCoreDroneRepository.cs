using Renting.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Renting.Drones
{
    public class EfCoreDroneRepository : EfCoreRepository<RentingDbContext, Drone, Guid>,
      IDroneRepository
    {
        public EfCoreDroneRepository(IDbContextProvider<RentingDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
