using Volo.Abp.Modularity;

namespace Renting;

[DependsOn(
    typeof(RentingApplicationModule),
    typeof(RentingDomainTestModule)
)]
public class RentingApplicationTestModule : AbpModule
{

}
