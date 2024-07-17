using System;
using System.Collections.Generic;
using System.Text;

using Volo.Abp.Application.Dtos;

namespace Renting.Reviews
{
    public class GetReviewsInput : PagedAndSortedResultRequestDto
    {
        public int? Rating { get; set; }
        public string? Content { get; set; }
        public DateTime? ReviewDate { get; set; }
        public Guid DroneId { get; set; }
        public Guid CustomerId { get; set; }
    }
}
