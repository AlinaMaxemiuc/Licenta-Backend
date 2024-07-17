using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using Volo.Abp;

namespace Renting.Drones
{
    public class Drone : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public string Name { get; set; }
        public string? Model { get; set; }
        public DroneUtility? Utility { get; set; }
        public DroneCategory? Category { get; set; }
        public DateTime ProductionYear { get; set; }
        public decimal PricePerDay { get; set; }
        public Guid? TenantId { get; init; }
        protected internal Drone(Guid id,
            string name,
            string? model,
            DroneUtility? utility,
            DroneCategory? category,
            DateTime productionYear,
            decimal pricePerDay) : base(id)
        {
            Id = id;
            SetName(name);
            Model = model;
            Utility = utility;
            Category = category;
            ProductionYear = productionYear;
            PricePerDay = pricePerDay;
        }
        private void SetName(string name)
        {
            Name = Check.NotNullOrWhiteSpace(
                name,
                nameof(name),
                maxLength: DroneConsts.MaxNameLength,
                minLength: DroneConsts.MinNameLength
                );
        }
    }
}
