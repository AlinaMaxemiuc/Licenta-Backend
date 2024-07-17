using Renting.Drones;

using System;

using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Renting.Rentals
{
    public class RentalItem : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public Guid RentalId { get; set; }
        public Guid DroneId { get; set; }
        public int DaysNumber { get; set; }
        public decimal PricePerDay { get; internal set; }
        public decimal Price { get; internal set; }
        public Guid? TenantId { get; init; }

        protected RentalItem() { }

        public RentalItem(Guid id,
            Guid rentalId,
            Guid droneId,
            int daysNumber,
            decimal pricePerDay) : base(id)
        {
            RentalId = rentalId;
            DroneId = droneId;
            DaysNumber = daysNumber;
            PricePerDay = pricePerDay;

            CalculatePrice();
        }

        public void SetPricePerDay(decimal pricePerDay)
        {
            if (pricePerDay < 0)
                throw new UserFriendlyException("Price per day must be greater than or equal to 0.");

            PricePerDay = pricePerDay;
            CalculatePrice();
        }

        public void SetDaysNumber(int daysNumber)
        {
            if (daysNumber <= 0)
            {
                throw new ArgumentException("Number of days must be greater than zero.");
            }

            DaysNumber = daysNumber;
            CalculatePrice();
        }

        private void CalculatePrice()
        {
            Price = DaysNumber * PricePerDay;
        }
    }
}
