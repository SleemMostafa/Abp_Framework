using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Wesaya.Data;

/* This is used if database provider does't define
 * IWesayaDbSchemaMigrator implementation.
 */
public class NullWesayaDbSchemaMigrator : IWesayaDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
