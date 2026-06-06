using System;
using System.Collections.Generic;
using System.Linq;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Wesaya.Localization;
using Wesaya.Menu.Exceptions;

namespace Wesaya.Menu;

public class MenuItem : FullAuditedAggregateRoot<Guid>
{
    public Guid CategoryId { get; private set; }

    public StrongLocalizedString Name { get; private set; } = null!;

    public OptionalLocalizedString Description { get; private set; } = OptionalLocalizedString.Empty();

    public decimal Price { get; private set; }

    public bool IsAvailable { get; private set; }

    public int PreparationTimeMinutes { get; private set; }

    private readonly List<ExtraItem> _extraItems = [];

    public IReadOnlyCollection<ExtraItem> ExtraItems => _extraItems;

    private MenuItem()
    {
    }

    private MenuItem(
        Guid id,
        Guid categoryId,
        StrongLocalizedString name,
        decimal price,
        OptionalLocalizedString? description,
        int preparationTimeMinutes,
        bool isAvailable)
        : base(id)
    {
        CategoryId = categoryId;
        IsAvailable = isAvailable;
        SetName(name);
        SetDescription(description);
        SetPrice(price);
        SetPreparationTime(preparationTimeMinutes);
    }

    public static MenuItem Create(
        Guid id,
        Guid categoryId,
        StrongLocalizedString name,
        decimal price,
        OptionalLocalizedString? description = null,
        int preparationTimeMinutes = 0,
        bool isAvailable = true)
    {
        return new MenuItem(
            id,
            categoryId,
            name,
            price,
            description,
            preparationTimeMinutes,
            isAvailable);
    }

    public void Update(
        Guid categoryId,
        StrongLocalizedString name,
        decimal price,
        OptionalLocalizedString? description,
        int preparationTimeMinutes,
        bool isAvailable)
    {
        MoveToCategory(categoryId);
        SetName(name);
        SetDescription(description);
        SetPrice(price);
        SetPreparationTime(preparationTimeMinutes);

        if (isAvailable)
        {
            MarkAsAvailable();
        }
        else
        {
            MarkAsUnavailable();
        }
    }

    public void MoveToCategory(Guid categoryId)
    {
        CategoryId = categoryId;
    }

    public void SetName(StrongLocalizedString name)
    {
        Name = name;
    }

    public void SetDescription(OptionalLocalizedString? description)
    {
        Description = description ?? OptionalLocalizedString.Empty();
    }

    public void SetPrice(decimal price)
    {
        if (price < 0)
        {
            throw new MenuItemPriceCannotBeNegativeException();
        }

        Price = price;
    }

    public void SetPreparationTime(int preparationTimeMinutes)
    {
        if (preparationTimeMinutes < 0)
        {
            throw new PreparationTimeCannotBeNegativeException();
        }

        PreparationTimeMinutes = preparationTimeMinutes;
    }

    public void AddExtraItem(StrongLocalizedString name, decimal price)
    {
        var extraItem = ExtraItem.Create(name, price);

        if (_extraItems.Any(item => item.Name.English == extraItem.Name.English || item.Name.Arabic == extraItem.Name.Arabic))
        {
            throw new ExtraItemAlreadyExistsException();
        }

        _extraItems.Add(extraItem);
    }

    public void RemoveExtraItem(string name)
    {
        name = Check.NotNullOrWhiteSpace(
            name,
            nameof(name),
            MenuConsts.MaxExtraItemNameLength);

        var extraItem = _extraItems.FirstOrDefault(item => item.Name.English == name || item.Name.Arabic == name);
        if (extraItem == null)
        {
            throw new ExtraItemNotFoundException();
        }

        _extraItems.Remove(extraItem);
    }

    public void ClearExtraItems()
    {
        _extraItems.Clear();
    }

    public void MarkAsAvailable()
    {
        IsAvailable = true;
    }

    public void MarkAsUnavailable()
    {
        IsAvailable = false;
    }
}
