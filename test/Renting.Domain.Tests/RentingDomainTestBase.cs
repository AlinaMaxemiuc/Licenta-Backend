using Volo.Abp.Modularity;

namespace Renting;

/* Inherit from this class for your domain layer tests. */
public abstract class RentingDomainTestBase<TStartupModule> : RentingTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
