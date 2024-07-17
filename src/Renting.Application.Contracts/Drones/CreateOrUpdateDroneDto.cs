using System;
using System.Collections.Generic;
using System.Text;

namespace Renting.Drones
{
    public class CreateOrUpdateDroneDto
    {
        public string Name { get; set; }
        public string? Model { get; set; }
        public DroneUtility? Utility { get; set; }
        public DroneCategory? Category { get; set; }
        public DateTime ProductionYear { get; set; }
        public decimal PricePerDay { get; set; }
    }
}
