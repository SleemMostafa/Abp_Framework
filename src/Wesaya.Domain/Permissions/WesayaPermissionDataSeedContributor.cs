using System.Threading.Tasks;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.PermissionManagement;

namespace Wesaya.Permissions;

public sealed class WesayaPermissionDataSeedContributor(IPermissionDataSeeder permissionDataSeeder)
    : IDataSeedContributor, ITransientDependency
{
    public async Task SeedAsync(DataSeedContext context)
    {
        await permissionDataSeeder.SeedAsync(
            RolePermissionValueProvider.ProviderName,
            "admin",
            [
                WesayaPermissions.MenuCategories.Default,
                WesayaPermissions.MenuCategories.Update,
                WesayaPermissions.MenuCategories.Delete
            ],
            null);
    }
}
