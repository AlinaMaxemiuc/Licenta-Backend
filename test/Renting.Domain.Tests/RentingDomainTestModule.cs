using Volo.Abp.Modularity;

namespace Renting;

[DependsOn(
    typeof(RentingDomainModule),
    typeof(RentingTestBaseModule)
)]
public class RentingDomainTestModule : AbpModule
{

}
