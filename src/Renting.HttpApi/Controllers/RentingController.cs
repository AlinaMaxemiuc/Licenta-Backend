using Renting.Localization;

using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;

namespace Renting.Controllers;

/* Inherit your controllers from this class.
 */
[IntegrationService]
public abstract class RentingController : AbpControllerBase
{
    protected RentingController()
    {
        LocalizationResource = typeof(RentingResource);
    }
}
