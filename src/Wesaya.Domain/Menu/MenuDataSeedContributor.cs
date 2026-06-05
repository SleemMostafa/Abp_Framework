using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Volo.Abp.Uow;

namespace Wesaya.Menu;

public class MenuDataSeedContributor(
    IRepository<MenuCategory, Guid> categoryRepository,
    IGuidGenerator guidGenerator)
    : IDataSeedContributor, ITransientDependency
{
    public ILogger<MenuDataSeedContributor> Logger { get; set; } = NullLogger<MenuDataSeedContributor>.Instance;

    [UnitOfWork]
    public virtual async Task SeedAsync(DataSeedContext context)
    {
        Logger.LogInformation("Started menu category seed.");

        var existingCategories = await categoryRepository.GetListAsync();

        await CreateCategoryIfNotExistsAsync(existingCategories, "Pizza", 1);
        await CreateCategoryIfNotExistsAsync(existingCategories, "Sweets", 2);
        await CreateCategoryIfNotExistsAsync(existingCategories, "Drinks", 3);

        Logger.LogInformation("Finished menu category seed.");
    }

    protected virtual async Task CreateCategoryIfNotExistsAsync(
        IReadOnlyCollection<MenuCategory> existingCategories,
        string name,
        int displayOrder)
    {
        if (existingCategories.Any(category => category.Name == name))
        {
            Logger.LogInformation("Menu category {CategoryName} already exists.", name);
            return;
        }

        await categoryRepository.InsertAsync(
            MenuCategory.Create(
                guidGenerator.Create(),
                name,
                displayOrder),
            autoSave: true);

        Logger.LogInformation("Created menu category {CategoryName}.", name);
    }
}
