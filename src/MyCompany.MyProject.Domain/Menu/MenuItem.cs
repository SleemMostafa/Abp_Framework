using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace MyCompany.MyProject.Menu;

public class MenuItem : FullAuditedAggregateRoot<Guid>, IMultiTenant
{
    public Guid? TenantId { get; private set; }

    public Guid CategoryId { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public string? Description { get; private set; }

    public decimal Price { get; private set; }

    public bool IsAvailable { get; private set; }

    public int PreparationTimeMinutes { get; private set; }

    private MenuItem()
    {
        Name = string.Empty;
    }

    public MenuItem(
        Guid id,
        Guid categoryId,
        string name,
        decimal price,
        Guid? tenantId = null,
        string? description = null,
        int preparationTimeMinutes = 0)
        : base(id)
    {
        TenantId = tenantId;
        CategoryId = categoryId;
        IsAvailable = true;
        SetName(name);
        SetDescription(description);
        SetPrice(price);
        SetPreparationTime(preparationTimeMinutes);
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
            throw new BusinessException("MyProject:MenuItemPriceCannotBeNegative");
        }

        Price = price;
    }

    public void SetPreparationTime(int preparationTimeMinutes)
    {
        if (preparationTimeMinutes < 0)
        {
            throw new BusinessException("MyProject:PreparationTimeCannotBeNegative");
        }

        PreparationTimeMinutes = preparationTimeMinutes;
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
