using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Wesaya.Localization;

namespace Wesaya.Menu.Seeders;

public sealed class MenuCategoryDataSeeder(
    IRepository<MenuCategory, Guid> categoryRepository,
    IGuidGenerator guidGenerator)
    : ITransientDependency
{
    public ILogger<MenuCategoryDataSeeder> Logger { get; set; } = NullLogger<MenuCategoryDataSeeder>.Instance;

    public async Task SeedAsync()
    {
        var existingCategories = await categoryRepository.GetListAsync();

        await CreateCategoryIfNotExistsAsync(existingCategories, "Pizza", "بيتزا", 1);
        await CreateCategoryIfNotExistsAsync(existingCategories, "Sweets", "حلويات", 2);
        await CreateCategoryIfNotExistsAsync(existingCategories, "Drinks", "مشروبات", 3);
    }

    private async Task CreateCategoryIfNotExistsAsync(
        IReadOnlyCollection<MenuCategory> existingCategories,
        string englishName,
        string arabicName,
        int displayOrder)
    {
        if (existingCategories.Any(category => category.Name.English == englishName || category.Name.Arabic == arabicName))
        {
            Logger.LogInformation("Menu category {CategoryName} already exists.", englishName);
            return;
        }

        await categoryRepository.InsertAsync(
            MenuCategory.Create(
                guidGenerator.Create(),
                StrongLocalizedString.Create(englishName, arabicName, MenuConsts.MaxCategoryNameLength),
                displayOrder),
            autoSave: true);

        Logger.LogInformation("Created menu category {CategoryName}.", englishName);
    }
}
