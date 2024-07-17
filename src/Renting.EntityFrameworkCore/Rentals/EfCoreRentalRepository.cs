using Renting.Drones;
using Renting.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Renting.Rentals
{
    public class EfCoreRentalRepository : EfCoreRepository<RentingDbContext, Rental, Guid>,
      IRentalRepository
    {
        public EfCoreRentalRepository(IDbContextProvider<RentingDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
