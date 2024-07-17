using Microsoft.Extensions.Logging;

using Renting.Customers;
using Renting.Drones;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net.Mail;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Uow;

namespace Renting.Rentals
{
    [IntegrationService]
    public class RentalAppService : RentingAppService, IRentalAppService
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly RentalManager _rentalManager;
        private readonly IDroneRepository _droneRepository;
        private readonly ICustomerRepository _customerRepository;
        public RentalAppService(IRentalRepository rentalRepository,
            RentalManager rentalManager,
            IDroneRepository droneRepository,
            ICustomerRepository customerRepository)
        {
            _rentalRepository = rentalRepository;
            _rentalManager = rentalManager;
            _droneRepository = droneRepository;
            _customerRepository = customerRepository;
        }
        public async Task<RentalDetailsDto> GetAsync(Guid id)
        {
            var rental = await _rentalRepository.GetAsync(id);

            await _rentalRepository.EnsureCollectionLoadedAsync(rental, r => r.RentalItems);

            var dto = ObjectMapper.Map<Rental, RentalDetailsDto>(rental);
            return dto;
        }

        public async Task<PagedResultDto<RentalDto>> GetListAsync(GetRentalsInput input)
        {
            var rentalDtos = new List<RentalDto>();
            var rentals = await _rentalRepository.GetPagedListAsync(input.SkipCount,
                input.MaxResultCount,
                input.Sorting,
                true);

            foreach (var rental in rentals)
            {
                await _rentalRepository.EnsureCollectionLoadedAsync(rental, r => r.RentalItems);

                var dto = ObjectMapper.Map<Rental, RentalDto>(rental);
                rentalDtos.Add(dto);
            }

            return new PagedResultDto<RentalDto>(rentals.Count, rentalDtos);
        }
        public async Task<RentalDetailsDto> CreateAsync(CreateOrUpdateRentalDto input)
        {
            var rental = ObjectMapper.Map<CreateOrUpdateRentalDto, Rental>(input);
            var daysNumber = (input.EndDay - input.StartDay).Days + 1;
            await _rentalRepository.EnsureCollectionLoadedAsync(rental, r => r.RentalItems);
            foreach (var item in input.RentalItems)
            {
                var existingItem = rental.RentalItems.FirstOrDefault(i => i.DroneId == item.DroneId);
                if (existingItem == null)
                {
                    var rentalItem = new RentalItem(
                        GuidGenerator.Create(),
                        rental.Id,
                        item.DroneId,
                        daysNumber,
                        item.PricePerDay
                    );
                    rental.RentalItems.Add(rentalItem);
                }
                else
                {
                    existingItem.SetDaysNumber(daysNumber);
                    existingItem.SetPricePerDay(item.PricePerDay);
                }
            }
            rental.CalculateTotal();
            await _rentalRepository.InsertAsync(rental);
            var customer = await _customerRepository.GetAsync(input.CustomerId);
            customer.Address = input.Address;
            await _customerRepository.UpdateAsync(customer);
            return ObjectMapper.Map<Rental, RentalDetailsDto>(rental);
        }

        public async Task<RentalDetailsDto> UpdateAsync(Guid id, CreateOrUpdateRentalDto input)
        {
            var rental = await _rentalRepository.GetAsync(id);

            await _rentalRepository.EnsureCollectionLoadedAsync(rental, r => r.RentalItems);

            var daysNumber = (input.EndDay - input.StartDay).Days + 1;

            rental.CustomerId = input.CustomerId;
            rental.StartDay = input.StartDay;
            rental.EndDay = input.EndDay;
            rental.Status = input.Status;
            rental.PaymentMethod = input.PaymentMethod;

            var existingItems = rental.RentalItems.ToList();
            foreach (var item in existingItems)
            {
                if (!input.RentalItems.Any(i => i.DroneId == item.DroneId))
                {
                    rental.RentalItems.Remove(item);
                }
            }

            foreach (var item in input.RentalItems)
            {
                var existingItem = rental.RentalItems.FirstOrDefault(i => i.DroneId == item.DroneId);
                if (existingItem == null)
                {
                    var rentalItem = new RentalItem(
                        GuidGenerator.Create(),
                        rental.Id,
                        item.DroneId,
                        daysNumber,
                        item.PricePerDay
                    );
                    rental.RentalItems.Add(rentalItem);
                }
                else
                {
                    existingItem.SetDaysNumber(daysNumber);
                    existingItem.SetPricePerDay(item.PricePerDay);
                }
            }

            rental.CalculateTotal();
            await _rentalRepository.UpdateAsync(rental);

            return ObjectMapper.Map<Rental, RentalDetailsDto>(rental);
        }

        public async Task<RentalDetailsDto> AddItemAsync(Guid rentalId, RentalItemDto input)
        {
            var rental = await _rentalRepository.GetAsync(rentalId);
            var drone = await _droneRepository.GetAsync(input.DroneId);

            await _rentalManager.AddItemAsync(rental,
                drone,
                input.DaysNumber,
                input.PricePerDay);

            return ObjectMapper.Map<Rental, RentalDetailsDto>(rental);
        }

        public async Task<RentalDetailsDto> RemoveItemAsync(Guid rentalId, Guid rentalItemId)
        {
            var rental = await _rentalRepository.GetAsync(rentalId);
            var rentalItem = rental.RentalItems.FirstOrDefault(item => item.Id == rentalItemId);
            if (rentalItem != null)
            {
                await _rentalManager.RemoveItemAsync(rental, rentalItem);
            }

            return ObjectMapper.Map<Rental, RentalDetailsDto>(rental);
        }
        public async Task DeleteAsync(Guid id)
        {
            await _rentalRepository.DeleteAsync(id);
        }
        public async Task<PagedResultDto<RentalDto>> GetRentalsByCustomerIdAsync(Guid customerId)
        {
            var rentals = await _rentalRepository.GetListAsync(r => r.CustomerId == customerId, true);

            var rentalDtos = new List<RentalDto>();
            foreach (var rental in rentals)
            {
                await _rentalRepository.EnsureCollectionLoadedAsync(rental, r => r.RentalItems);

                var rentalDto = ObjectMapper.Map<Rental, RentalDto>(rental);
                rentalDtos.Add(rentalDto);
            }

            return new PagedResultDto<RentalDto>(rentals.Count, rentalDtos);
        }
        public async Task<bool> CheckAvailabilityAsync(Guid droneId, DateTime startDate, DateTime endDate)
        {
            var rentals = await _rentalRepository.GetListAsync(r =>
                r.RentalItems.Any(ri => ri.DroneId == droneId) &&
                ((startDate >= r.StartDay && startDate <= r.EndDay) ||
                (endDate >= r.StartDay && endDate <= r.EndDay) ||
                (startDate <= r.StartDay && endDate >= r.EndDay))
            );

            return !rentals.Any();
        }

        public async Task<AvailabilityResultDto> CheckAvailability(CheckAvailabilityDto input)
        {
            var isAvailable = await CheckAvailabilityAsync(input.DroneId, input.StartDate, input.EndDate);
            return new AvailabilityResultDto { Available = isAvailable };
        }
    }
}
