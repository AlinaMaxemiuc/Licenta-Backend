using Renting.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Renting.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(RentingEntityFrameworkCoreModule),
    typeof(RentingApplicationContractsModule)
    )]
public class RentingDbMigratorModule : AbpModule
{
}
