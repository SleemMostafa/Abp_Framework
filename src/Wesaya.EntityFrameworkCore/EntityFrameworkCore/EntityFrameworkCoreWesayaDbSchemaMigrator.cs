using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Wesaya.Data;
using Volo.Abp.DependencyInjection;

namespace Wesaya.EntityFrameworkCore;

public class EntityFrameworkCoreWesayaDbSchemaMigrator(IServiceProvider serviceProvider)
    : IWesayaDbSchemaMigrator, ITransientDependency
{
    public async Task MigrateAsync()
    {
        /* We intentionally resolve the WesayaDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await serviceProvider
            .GetRequiredService<WesayaDbContext>()
            .Database
            .MigrateAsync();
    }
}
