using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Volo.Abp.Domain.Services;
using Volo.Abp.Guids;
using Volo.Abp;
using System.Diagnostics.CodeAnalysis;

namespace Renting.Customers
{
    public class CustomerManager : DomainService
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerManager(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task<Customer> CreateAsync(string? firstName,
             string? lastName,
             string? email,
             string? phoneNumber,
             string? address)
        {

            var customer = new Customer(GuidGenerator.Create(),
                firstName,
                lastName,
                email,
                phoneNumber,
                address);

           // await _customerRepository.InsertAsync(customer);
            return customer;

        }
        public async Task<Customer> UpdateAsync(Customer customer,
            string? firstName,
             string? lastName,
             string? email,
             string? phoneNumber,
             string? address
            )
        {
            customer.FirstName = firstName;
            customer.LastName = lastName;
            customer.Email = email;
            customer.PhoneNumber = phoneNumber;
            customer.Address = address;

            return customer;
        }
    }
}
