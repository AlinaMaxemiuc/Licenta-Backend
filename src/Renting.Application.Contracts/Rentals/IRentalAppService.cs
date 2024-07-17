using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Volo.Abp;
using Volo.Abp.Application.Services;

namespace Renting.Rentals
{
    [IntegrationService]
    public interface IRentalAppService :
    ICrudAppService<
        RentalDetailsDto,
        RentalDto,
        Guid,
        GetRentalsInput,
        CreateOrUpdateRentalDto,
        CreateOrUpdateRentalDto>
    {
    }
}
