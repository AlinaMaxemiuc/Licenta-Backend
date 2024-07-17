using Renting.Customers;
using Renting.Drones;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Renting.Reviews
{
    public class Review : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public int? Rating { get; set; }
        public string? Content {  get; set; }
        public DateTime? ReviewDate { get; set; }
        public Guid DroneId { get; set; }
        public Guid CustomerId { get; set; }
        public Guid? TenantId { get; init; }
        public Review(Guid id,
            int? rating,
            string? content,
            DateTime? reviewDate,
            Guid droneId,
            Guid customerId) : base(id)
        {
            Rating = rating;
            Content = content;
            ReviewDate = reviewDate;
            DroneId = droneId;
            CustomerId = customerId;
        }
    }
}
