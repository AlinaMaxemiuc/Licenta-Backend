using AutoMapper;

using Renting.Customers;
using Renting.Drones;
using Renting.Rentals;
using Renting.Reviews;

namespace Renting;

public class RentingApplicationAutoMapperProfile : Profile
{
    public RentingApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<Customer, CustomerDto>();
        CreateMap<Customer, CustomerDetailsDto>();
        CreateMap<CreateOrUpdateCustomerDto, Customer>();

        CreateMap<Drone, DroneDto>();
        CreateMap<Drone, DroneDetailsDto>();
        CreateMap<CreateOrUpdateDroneDto, Drone>();

        CreateMap<Review, ReviewDto>();
        CreateMap<Review, ReviewDetailsDto>();
        CreateMap<CreateOrUpdateReviewDto, Review>();

        CreateMap<Rental, RentalDto>();
        CreateMap<Rental, RentalDetailsDto>();
        CreateMap<CreateOrUpdateRentalDto, Rental>();
        CreateMap<CreateOrUpdateRentalItemDto, RentalItem>();
        CreateMap<RentalItem, RentalItemDto>();

    }
}
