using System;
using System.Collections.Generic;
using System.Text;
using Renting.Localization;
using Volo.Abp.Application.Services;

namespace Renting;

/* Inherit your application services from this class.
 */
public abstract class RentingAppService : ApplicationService
{
    protected RentingAppService()
    {
        LocalizationResource = typeof(RentingResource);
    }
}
