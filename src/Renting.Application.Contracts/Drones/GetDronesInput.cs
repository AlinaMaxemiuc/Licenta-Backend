using System;
using System.Collections.Generic;
using System.Text;

using Volo.Abp.Application.Dtos;

namespace Renting.Drones
{
    public class GetDronesInput : PagedAndSortedResultRequestDto
    {
        public string? Name { get; set; }
        public string? Model { get; set; }
        public DroneUtility? Utility { get; set; }
        public DroneCategory? Category { get; set; }
        public DateTime? ProductionYear { get; set; }
        public decimal? PricePerDay { get; set; }
    }
}
