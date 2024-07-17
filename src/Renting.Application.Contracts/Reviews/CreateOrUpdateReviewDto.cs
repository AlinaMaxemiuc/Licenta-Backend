using System;
using System.Collections.Generic;
using System.Text;

namespace Renting.Reviews
{
    public class CreateOrUpdateReviewDto
    {
        public int? Rating { get; set; }
        public string? Content { get; set; }
        public DateTime? ReviewDate { get; set; }
        public Guid DroneId { get; set; }
        public Guid CustomerId { get; set; }
    }
}
