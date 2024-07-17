using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Volo.Abp.Domain.Repositories;

namespace Renting.Reviews
{
    public interface IReviewRepository : IRepository<Review, Guid>
    {
    }
}
