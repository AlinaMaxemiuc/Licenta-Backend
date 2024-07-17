using System;
using System.Collections.Generic;
using System.Text;

using Volo.Abp.Application.Dtos;

namespace Renting.Rentals
{
    public class RentalItemDto : AuditedEntityDto<Guid>
    {
        public Guid DroneId { get; set; }
        public int DaysNumber { get; set; }
        public decimal PricePerDay { get; set; }
        public decimal Price { get; set; }
    }
}
