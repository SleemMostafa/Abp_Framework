using System;
using Volo.Abp.Domain.Entities.Auditing;
using Wesaya.Localization;

namespace Wesaya.Menu;

public sealed class MenuCategory : FullAuditedAggregateRoot<Guid>
{
    public StrongLocalizedString Name { get; private set; } = null!;

    public int DisplayOrder { get; private set; }

    public bool IsActive { get; private set; }

    private MenuCategory()
    {
    }

    private MenuCategory(
        Guid id,
        StrongLocalizedString name,
        int displayOrder,
        bool isActive)
        : base(id)
    {
        DisplayOrder = displayOrder;
        IsActive = isActive;
        SetName(name);
    }

    public static MenuCategory Create(
        Guid id,
        StrongLocalizedString name,
        int displayOrder = 0,
        bool isActive = true)
    {
        return new MenuCategory(
            id,
            name,
            displayOrder,
            isActive);
    }

    public void Update(
        StrongLocalizedString name,
        int displayOrder,
        bool isActive)
    {
        SetName(name);
        SetDisplayOrder(displayOrder);

        if (isActive)
        {
            Activate();
        }
        else
        {
            Deactivate();
        }
    }

    public void SetName(StrongLocalizedString name)
    {
        Name = name;
    }

    public void SetDisplayOrder(int displayOrder)
    {
        DisplayOrder = displayOrder;
    }

    public void Activate()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }
}
