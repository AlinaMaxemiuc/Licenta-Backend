using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

using Volo.Abp.MultiTenancy;

using Volo.Abp;

namespace Renting.Customers
{
    public class Customer : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public Guid? TenantId { get; init; }
        protected internal Customer(Guid id,
            string? firstName,
            string? lastName,
            string? email,
            string? phoneNumber,
            string? address) : base(id)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;

        }
    }
}
