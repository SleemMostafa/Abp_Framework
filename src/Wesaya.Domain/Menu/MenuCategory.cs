using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Wesaya.Menu;

public class MenuCategory : FullAuditedAggregateRoot<Guid>
{
    public string Name { get; private set; } = string.Empty;

    public int DisplayOrder { get; private set; }

    public bool IsActive { get; private set; }

    private MenuCategory()
    {
        Name = string.Empty;
    }

    private MenuCategory(
        Guid id,
        string name,
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
        string name,
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
        string name,
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

    public void SetName(string name)
    {
        Name = Check.NotNullOrWhiteSpace(
            name,
            nameof(name),
            MenuConsts.MaxCategoryNameLength);
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
