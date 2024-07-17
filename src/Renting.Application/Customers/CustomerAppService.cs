using AutoMapper.Internal.Mappers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Renting.Customers
{
    [IntegrationService]
    public class CustomerAppService : RentingAppService, ICustomerAppService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly CustomerManager _customerManager;

        public CustomerAppService(ICustomerRepository customerRepository, CustomerManager customerManager)
        {
            _customerRepository = customerRepository;
            _customerManager = customerManager;
        }
        public async Task<CustomerDetailsDto> GetAsync(Guid id)
        {
            var customer = await _customerRepository.GetAsync(id);

            var dto = ObjectMapper.Map<Customer, CustomerDetailsDto>(customer);
            return dto;
        }
        public async Task<PagedResultDto<CustomerDto>> GetListAsync(GetCustomersInput input)
        {
            var customerDtos = new List<CustomerDto>();
            var customers = await _customerRepository.GetPagedListAsync(input.SkipCount,
                input.MaxResultCount,
                input.Sorting,
                true);

            foreach (var customer in customers)
            {
                var dto = ObjectMapper.Map<Customer, CustomerDto>(customer);
                customerDtos.Add(dto);
            }

            return new PagedResultDto<CustomerDto>(customers.Count, customerDtos);
        }

        public async Task<CustomerDetailsDto> CreateAsync(CreateOrUpdateCustomerDto input)
        {
            var customer = await _customerManager.CreateAsync(input.FirstName,
                input.LastName,
                input.Email,
                input.PhoneNumber,
                input.Address);

            customer = await _customerRepository.InsertAsync(customer);
            return ObjectMapper.Map<Customer, CustomerDetailsDto>(customer); 
        }

        public async Task<CustomerDetailsDto> UpdateAsync(Guid id, CreateOrUpdateCustomerDto input)
        {
            var customer = await _customerRepository.GetAsync(id);

            customer.FirstName = input.FirstName;
            customer.LastName = input.LastName;
            customer.Email = input.Email;
            customer.PhoneNumber = input.PhoneNumber;
            customer.Address = input.Address;

            customer = await _customerRepository.UpdateAsync(customer);

            var dto = ObjectMapper.Map<Customer, CustomerDetailsDto>(customer);
            return dto;
        }

        public async Task DeleteAsync(Guid id)
        {
            await _customerRepository.DeleteAsync(id);
        }
    }
}
