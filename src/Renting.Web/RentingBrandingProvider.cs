using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace Renting.Web;

[Dependency(ReplaceServices = true)]
public class RentingBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "Renting";
}
