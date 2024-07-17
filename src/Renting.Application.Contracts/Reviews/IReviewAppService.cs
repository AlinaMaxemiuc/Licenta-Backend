using System;
using System.Collections.Generic;
using System.Text;

using Volo.Abp;
using Volo.Abp.Application.Services;

namespace Renting.Reviews
{
    [IntegrationService]
    public interface IReviewAppService :
    ICrudAppService<
        ReviewDetailsDto,
        ReviewDto,
        Guid,
        GetReviewsInput,
        CreateOrUpdateReviewDto,
        CreateOrUpdateReviewDto>
    { }
}
