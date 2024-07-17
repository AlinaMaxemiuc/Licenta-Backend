using Renting.Drones;
using Renting.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Volo.Abp.Domain.Repositories.EntityFrameworkCore;

using Volo.Abp.EntityFrameworkCore;

namespace Renting.Reviews
{
    public class EfCoreReviewRepository : EfCoreRepository<RentingDbContext, Review, Guid>,
      IReviewRepository
    {
        public EfCoreReviewRepository(IDbContextProvider<RentingDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
