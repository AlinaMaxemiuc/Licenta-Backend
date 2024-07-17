using Renting.Drones;

using System.Linq;
using System.Threading.Tasks;

using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace Renting.Rentals
{
    public class RentalManager : DomainService
    {
        private readonly IRentalRepository _rentalRepository;

        public RentalManager(IRentalRepository rentalRepository)
        {
            _rentalRepository = rentalRepository;
        }

        public async Task AddItemAsync(Rental rental,
             Drone drone,
             int daysNumber,
             decimal pricePerDay)
        {
            Check.NotNull(rental, nameof(rental));
            Check.NotNull(drone, nameof(drone));

            await _rentalRepository.EnsureCollectionLoadedAsync(rental, r => r.RentalItems);

            var rentalItem = rental.RentalItems.FirstOrDefault(item => item.DroneId == drone.Id);
            if (rentalItem == null)
            {
                rentalItem = new RentalItem(GuidGenerator.Create(),
                    rental.Id,
                    drone.Id,
                    daysNumber,
                    pricePerDay);
                rental.RentalItems.Add(rentalItem);
            }
            else
            {
                rentalItem.SetDaysNumber(daysNumber);
                rentalItem.SetPricePerDay(pricePerDay);
            }

            rental.CalculateTotal();
            await _rentalRepository.UpdateAsync(rental);
        }


        public async Task RemoveItemAsync(Rental rental, RentalItem rentalItem)
        {
            Check.NotNull(rental, nameof(rental));
            Check.NotNull(rentalItem, nameof(rentalItem));

            await _rentalRepository.EnsureCollectionLoadedAsync(rental, r => r.RentalItems);

            var rentalItemToRemove = rental.RentalItems.FirstOrDefault(ri => ri.Id == rentalItem.Id);
            if (rentalItemToRemove != null)
            {
                rental.RentalItems.Remove(rentalItemToRemove);
            }

            rental.CalculateTotal();
            await _rentalRepository.UpdateAsync(rental);
        }

        public async Task UpdateStatusAsync(Rental rental, RentalStatus status)
        {
            Check.NotNull(rental, nameof(rental));
            rental.UpdateStatus(status);
            await _rentalRepository.UpdateAsync(rental);
        }

        public async Task CancelRentalAsync(Rental rental)
        {
            await UpdateStatusAsync(rental, RentalStatus.Cancelled);
        }
    }
}
