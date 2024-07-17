using Renting.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Renting.Customers
{
    public class EfCoreCustomerRepository : EfCoreRepository<RentingDbContext, Customer, Guid>,
        ICustomerRepository
    {
        public EfCoreCustomerRepository(IDbContextProvider<RentingDbContext> dbContextProvider) : base(dbContextProvider)
        { }
    }
}
