using System;
using System.Collections.Generic;
using System.Text;

using Volo.Abp.Application.Services;

namespace Renting.Customers
{
    public interface ICustomerAppService :
    ICrudAppService<
        CustomerDetailsDto,
        CustomerDto,
        Guid,
        GetCustomersInput,
        CreateOrUpdateCustomerDto,
        CreateOrUpdateCustomerDto>
    { }
}
