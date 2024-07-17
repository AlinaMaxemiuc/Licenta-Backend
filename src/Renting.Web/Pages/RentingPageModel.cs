using Renting.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Renting.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class RentingPageModel : AbpPageModel
{
    protected RentingPageModel()
    {
        LocalizationResourceType = typeof(RentingResource);
    }
}
