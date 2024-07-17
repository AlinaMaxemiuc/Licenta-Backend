using System;
using System.Collections.Generic;
using System.Text;

using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Renting.Drones
{
    public class DroneDetailsDto : EntityDto<Guid>, IHasConcurrencyStamp
    {
        public string Name { get; set; }
        public string? Model { get; set; }
        public DroneUtility? Utility { get; set; }
        public DroneCategory? Category { get; set; }
        public DateTime ProductionYear { get; set; }
        public decimal PricePerDay { get; set; }
        public string ConcurrencyStamp { get; set; }
    }
}
