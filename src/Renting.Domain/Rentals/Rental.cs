using Renting.Customers;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Renting.Rentals
{
    public class Rental : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public Guid CustomerId { get; set; }
        public DateTime StartDay { get; set; }
        public DateTime EndDay { get; set; }
        public decimal Total { get; set; }
        public RentalStatus Status { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

        public Guid? TenantId { get; init; }

        public ICollection<RentalItem> RentalItems { get; internal set; }

        protected Rental()
        {
            RentalItems = new Collection<RentalItem>();
        }

        public Rental(Guid id,
            Customer customer,
            DateTime startDay,
            DateTime endDay,
            RentalStatus status,
            PaymentMethod paymentMethod) : base(id)
        {
            CustomerId = customer.Id;
            StartDay = startDay;
            EndDay = endDay;
            Status = status;
            PaymentMethod = paymentMethod;
            RentalItems = new Collection<RentalItem>();

            CalculateTotal();
        }

        internal void UpdateStatus(RentalStatus status)
        {
            Status = status;
        }

        public void CalculateTotal()
        {
            Total = RentalItems.Sum(item => item.Price);
        }

    }
}
