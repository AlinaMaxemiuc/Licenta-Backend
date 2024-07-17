using System;
using System.Collections.Generic;
using System.Text;

using Volo.Abp.Application.Dtos;

namespace Renting.Rentals
{
    public class RentalDto : AuditedEntityDto<Guid>
    {
        public Guid CustomerId { get; set; }
        public DateTime StartDay { get; set; }
        public DateTime EndDay { get; set; }
        public decimal Total { get; set; }
        public RentalStatus Status { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public List<RentalItemDto> RentalItems { get; set; } = new();
    }
}
