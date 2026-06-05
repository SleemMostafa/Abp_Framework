using System;
using System.Collections.Generic;
using System.Linq;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Wesaya.Menu;

public class MenuItem : FullAuditedAggregateRoot<Guid>
{
    public Guid CategoryId { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public string? Description { get; private set; }

    public decimal Price { get; private set; }

    public bool IsAvailable { get; private set; }

    public int PreparationTimeMinutes { get; private set; }

    private readonly List<ExtraItem> _extraItems = [];

    public IReadOnlyCollection<ExtraItem> ExtraItems => _extraItems;

    private MenuItem()
    {
        Name = string.Empty;
    }

    private MenuItem(
        Guid id,
        Guid categoryId,
        string name,
        decimal price,
        string? description,
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
        string name,
        decimal price,
        string? description = null,
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
        string name,
        decimal price,
        string? description,
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

    public void SetName(string name)
    {
        Name = Check.NotNullOrWhiteSpace(
            name,
            nameof(name),
            MenuConsts.MaxItemNameLength);
    }

    public void SetDescription(string? description)
    {
        Description = Check.Length(
            description,
            nameof(description),
            MenuConsts.MaxItemDescriptionLength);
    }

    public void SetPrice(decimal price)
    {
        if (price < 0)
        {
            throw new BusinessException("Wesaya:MenuItemPriceCannotBeNegative");
        }

        Price = price;
    }

    public void SetPreparationTime(int preparationTimeMinutes)
    {
        if (preparationTimeMinutes < 0)
        {
            throw new BusinessException("Wesaya:PreparationTimeCannotBeNegative");
        }

        PreparationTimeMinutes = preparationTimeMinutes;
    }

    public void AddExtraItem(string name, decimal price)
    {
        var extraItem = ExtraItem.Create(name, price);

        if (_extraItems.Any(item => item.Name == extraItem.Name))
        {
            throw new BusinessException("Wesaya:ExtraItemAlreadyExists");
        }

        _extraItems.Add(extraItem);
    }

    public void RemoveExtraItem(string name)
    {
        name = Check.NotNullOrWhiteSpace(
            name,
            nameof(name),
            MenuConsts.MaxExtraItemNameLength);

        var extraItem = _extraItems.FirstOrDefault(item => item.Name == name);
        if (extraItem == null)
        {
            throw new BusinessException("Wesaya:ExtraItemNotFound");
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
