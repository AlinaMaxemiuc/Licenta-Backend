using System;
using System.Collections.Generic;
using System.Text;

namespace Renting.Rentals
{
    public class CheckAvailabilityDto
    {
        public Guid DroneId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

}
