using Volo.Abp.Modularity;

namespace Renting;

public abstract class RentingApplicationTestBase<TStartupModule> : RentingTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
