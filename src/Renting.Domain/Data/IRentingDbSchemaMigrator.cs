using System.Threading.Tasks;

namespace Renting.Data;

public interface IRentingDbSchemaMigrator
{
    Task MigrateAsync();
}
