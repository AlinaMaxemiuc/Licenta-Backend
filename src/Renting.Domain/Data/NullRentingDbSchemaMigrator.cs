using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Renting.Data;

/* This is used if database provider does't define
 * IRentingDbSchemaMigrator implementation.
 */
public class NullRentingDbSchemaMigrator : IRentingDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
