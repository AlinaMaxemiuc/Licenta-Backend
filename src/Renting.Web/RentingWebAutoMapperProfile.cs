using AutoMapper;

using Renting.Customers;
using Renting.Drones;
using Renting.Reviews;

namespace Renting.Web;

public class RentingWebAutoMapperProfile : Profile
{
    public RentingWebAutoMapperProfile()
    {
        //Define your AutoMapper configuration here for the Web project.

        CreateMap<Customer, CustomerDto>();
        CreateMap<CustomerDto, CreateOrUpdateCustomerDto>();
        CreateMap<CustomerDetailsDto, CreateOrUpdateCustomerDto>();
        CreateMap<CreateOrUpdateCustomerDto, Customer>();

        CreateMap<Drone, DroneDto>();
        CreateMap<DroneDto, CreateOrUpdateDroneDto>();
        CreateMap<DroneDetailsDto, CreateOrUpdateDroneDto>();
        CreateMap<CreateOrUpdateDroneDto, Drone>();

        CreateMap<Review, ReviewDto>();
        CreateMap<ReviewDto, CreateOrUpdateReviewDto>();
        CreateMap<ReviewDetailsDto, CreateOrUpdateReviewDto>();
        CreateMap<CreateOrUpdateReviewDto, Review>();
    }
}
