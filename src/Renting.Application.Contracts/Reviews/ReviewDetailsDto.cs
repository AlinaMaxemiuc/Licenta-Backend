using System;
using System.Collections.Generic;
using System.Text;

using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Renting.Reviews
{
    public class ReviewDetailsDto : EntityDto<Guid>, IHasConcurrencyStamp
    {
        public int? Rating { get; set; }
        public string? Content { get; set; }
        public DateTime? ReviewDate { get; set; }
        public Guid DroneId { get; set; }
        public Guid CustomerId { get; set; }
        public string ConcurrencyStamp { get; set; }
    }
}
