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
using Wesaya.Menu.Exceptions;

namespace Wesaya.Menu.Seeders;

public sealed class MenuItemDataSeeder(
    IRepository<MenuCategory, Guid> categoryRepository,
    IRepository<MenuItem, Guid> itemRepository,
    IGuidGenerator guidGenerator)
    : ITransientDependency
{
    public ILogger<MenuItemDataSeeder> Logger { get; set; } = NullLogger<MenuItemDataSeeder>.Instance;

    public async Task SeedAsync()
    {
        var categories = await categoryRepository.GetListAsync();
        var existingItems = await itemRepository.GetListAsync();

        var pizzaCategoryId = GetCategoryId(categories, "Pizza");
        var sweetsCategoryId = GetCategoryId(categories, "Sweets");
        var drinksCategoryId = GetCategoryId(categories, "Drinks");

        await CreateItemIfNotExistsAsync(existingItems, pizzaCategoryId, "Margherita Pizza", "بيتزا مارجريتا", "Classic pizza with mozzarella and tomato sauce.", "بيتزا كلاسيكية بالموتزاريلا وصلصة الطماطم.", 120, 15,
            Extra("Extra Cheese", "جبن إضافي", 20),
            Extra("Olives", "زيتون", 10));

        await CreateItemIfNotExistsAsync(existingItems, pizzaCategoryId, "Pepperoni Pizza", "بيتزا بيبروني", "Pepperoni pizza with mozzarella cheese.", "بيتزا بيبروني مع جبن الموتزاريلا.", 155, 18,
            Extra("Extra Cheese", "جبن إضافي", 20),
            Extra("Mushrooms", "مشروم", 15));

        await CreateItemIfNotExistsAsync(existingItems, pizzaCategoryId, "Chicken Ranch Pizza", "بيتزا دجاج رانش", "Chicken pizza with ranch sauce.", "بيتزا دجاج مع صوص الرانش.", 170, 20,
            Extra("Ranch Sauce", "صوص رانش", 12),
            Extra("Jalapeno", "هالبينو", 10));

        await CreateItemIfNotExistsAsync(existingItems, pizzaCategoryId, "Veggie Pizza", "بيتزا خضار", "Pizza with peppers, olives, mushrooms, and onion.", "بيتزا بالفلفل والزيتون والمشروم والبصل.", 135, 17,
            Extra("Olives", "زيتون", 10),
            Extra("Mushrooms", "مشروم", 15));

        await CreateItemIfNotExistsAsync(existingItems, pizzaCategoryId, "Seafood Pizza", "بيتزا سي فود", "Seafood pizza with shrimp and calamari.", "بيتزا مأكولات بحرية بالجمبري والكاليماري.", 210, 20,
            Extra("Shrimp", "جمبري", 35),
            Extra("Extra Cheese", "جبن إضافي", 20));

        await CreateItemIfNotExistsAsync(existingItems, pizzaCategoryId, "Cheese Lovers Pizza", "بيتزا عشاق الجبن", "Pizza with a rich mix of cheeses.", "بيتزا بخليط غني من الجبن.", 160, 18,
            Extra("Extra Mozzarella", "موتزاريلا إضافية", 25),
            Extra("Cheddar", "شيدر", 20));

        await CreateItemIfNotExistsAsync(existingItems, sweetsCategoryId, "Chocolate Cake", "كيك شوكولاتة", "Soft chocolate cake slice.", "قطعة كيك شوكولاتة طرية.", 75, 5,
            Extra("Chocolate Sauce", "صوص شوكولاتة", 12),
            Extra("Ice Cream", "آيس كريم", 20));

        await CreateItemIfNotExistsAsync(existingItems, sweetsCategoryId, "Cheesecake", "تشيز كيك", "Cheesecake with berry topping.", "تشيز كيك مع صوص التوت.", 85, 5,
            Extra("Berry Sauce", "صوص توت", 12),
            Extra("Ice Cream", "آيس كريم", 20));

        await CreateItemIfNotExistsAsync(existingItems, sweetsCategoryId, "Kunafa", "كنافة", "Fresh kunafa with syrup.", "كنافة طازجة مع الشربات.", 65, 7,
            Extra("Cream", "قشطة", 15),
            Extra("Nuts", "مكسرات", 18));

        await CreateItemIfNotExistsAsync(existingItems, sweetsCategoryId, "Basbousa", "بسبوسة", "Traditional basbousa slice.", "قطعة بسبوسة تقليدية.", 45, 4,
            Extra("Cream", "قشطة", 15),
            Extra("Nuts", "مكسرات", 18));

        await CreateItemIfNotExistsAsync(existingItems, sweetsCategoryId, "Rice Pudding", "أرز باللبن", "Rice pudding with cinnamon.", "أرز باللبن مع القرفة.", 40, 4,
            Extra("Nuts", "مكسرات", 18),
            Extra("Raisins", "زبيب", 8));

        await CreateItemIfNotExistsAsync(existingItems, sweetsCategoryId, "Donut", "دونات", "Glazed donut.", "دونات بالجليز.", 35, 3,
            Extra("Chocolate Sauce", "صوص شوكولاتة", 12),
            Extra("Caramel Sauce", "صوص كراميل", 12));

        await CreateItemIfNotExistsAsync(existingItems, drinksCategoryId, "Cola", "كولا", "Cold cola drink.", "مشروب كولا بارد.", 30, 2,
            Extra("Ice", "ثلج", 0),
            Extra("Lemon Slice", "شريحة ليمون", 3));

        await CreateItemIfNotExistsAsync(existingItems, drinksCategoryId, "Orange Juice", "عصير برتقال", "Fresh orange juice.", "عصير برتقال طازج.", 45, 4,
            Extra("Ice", "ثلج", 0),
            Extra("Mint", "نعناع", 5));

        await CreateItemIfNotExistsAsync(existingItems, drinksCategoryId, "Mango Juice", "عصير مانجو", "Fresh mango juice.", "عصير مانجو طازج.", 55, 4,
            Extra("Ice", "ثلج", 0),
            Extra("Milk", "حليب", 8));

        await CreateItemIfNotExistsAsync(existingItems, drinksCategoryId, "Lemon Mint", "ليمون بالنعناع", "Fresh lemon juice with mint.", "عصير ليمون طازج بالنعناع.", 45, 4,
            Extra("Ice", "ثلج", 0),
            Extra("Extra Mint", "نعناع إضافي", 5));

        await CreateItemIfNotExistsAsync(existingItems, drinksCategoryId, "Turkish Coffee", "قهوة تركي", "Hot Turkish coffee.", "قهوة تركي ساخنة.", 40, 5,
            Extra("Extra Sugar", "سكر إضافي", 0),
            Extra("Milk", "حليب", 8));

        await CreateItemIfNotExistsAsync(existingItems, drinksCategoryId, "Water", "مياه", "Cold bottled water.", "زجاجة مياه باردة.", 15, 1,
            Extra("Ice", "ثلج", 0),
            Extra("Lemon Slice", "شريحة ليمون", 3));
    }

    private async Task CreateItemIfNotExistsAsync(
        IReadOnlyCollection<MenuItem> existingItems,
        Guid categoryId,
        string englishName,
        string arabicName,
        string englishDescription,
        string arabicDescription,
        decimal price,
        int preparationTimeMinutes,
        params SeedExtraItem[] extraItems)
    {
        if (existingItems.Any(item => item.Name.English == englishName || item.Name.Arabic == arabicName))
        {
            Logger.LogInformation("Menu item {ItemName} already exists.", englishName);
            return;
        }

        var item = MenuItem.Create(
            guidGenerator.Create(),
            categoryId,
            Strong(englishName, arabicName, MenuConsts.MaxItemNameLength),
            price,
            Optional(englishDescription, arabicDescription, MenuConsts.MaxItemDescriptionLength),
            preparationTimeMinutes);

        foreach (var extraItem in extraItems)
        {
            item.AddExtraItem(
                Strong(extraItem.EnglishName, extraItem.ArabicName, MenuConsts.MaxExtraItemNameLength),
                extraItem.Price);
        }

        await itemRepository.InsertAsync(item, autoSave: true);

        Logger.LogInformation("Created menu item {ItemName}.", englishName);
    }

    private static Guid GetCategoryId(IEnumerable<MenuCategory> categories, string englishName)
    {
        var category = categories.FirstOrDefault(category =>
            category.Name.English == englishName || category.Name.Arabic == englishName);

        if (category == null)
        {
            throw new MenuCategoryNotFoundException();
        }

        return category.Id;
    }

    private static StrongLocalizedString Strong(string english, string arabic, int maxLength)
    {
        return StrongLocalizedString.Create(english, arabic, maxLength);
    }

    private static OptionalLocalizedString Optional(string? english, string? arabic, int maxLength)
    {
        return OptionalLocalizedString.Create(english, arabic, maxLength);
    }

    private static SeedExtraItem Extra(string englishName, string arabicName, decimal price)
    {
        return new SeedExtraItem(englishName, arabicName, price);
    }
}
