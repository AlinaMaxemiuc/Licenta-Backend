using System;
using System.Collections.Generic;
using System.Text;

namespace Renting.Rentals
{
    public class CreateOrUpdateRentalDto
    {
        public Guid CustomerId { get; set; }
        public DateTime StartDay { get; set; }
        public DateTime EndDay { get; set; }
        public RentalStatus Status { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public List<CreateOrUpdateRentalItemDto> RentalItems { get; set; } = new();
        public string Address { get; set; }
    }
    public class CreateOrUpdateRentalItemDto
    {
        public Guid DroneId { get; set; }
        public decimal PricePerDay { get; set; }
    }
}
