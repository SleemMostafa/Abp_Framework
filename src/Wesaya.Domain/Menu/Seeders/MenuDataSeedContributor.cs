using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;

namespace Wesaya.Menu.Seeders;

public class MenuDataSeedContributor(
    MenuCategoryDataSeeder categoryDataSeeder,
    MenuItemDataSeeder itemDataSeeder)
    : IDataSeedContributor, ITransientDependency
{
    public ILogger<MenuDataSeedContributor> Logger { get; set; } = NullLogger<MenuDataSeedContributor>.Instance;

    [UnitOfWork]
    public virtual async Task SeedAsync(DataSeedContext context)
    {
        Logger.LogInformation("Started menu seed.");

        await categoryDataSeeder.SeedAsync();
        await itemDataSeeder.SeedAsync();

        Logger.LogInformation("Finished menu seed.");
    }
}
