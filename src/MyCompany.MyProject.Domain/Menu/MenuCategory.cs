using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace MyCompany.MyProject.Menu;

public class MenuCategory : FullAuditedAggregateRoot<Guid>, IMultiTenant
{
    public Guid? TenantId { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public int DisplayOrder { get; private set; }

    public bool IsActive { get; private set; }

    public Guid? ParentId { get; private set; }

    private MenuCategory()
    {
        Name = string.Empty;
    }

    public MenuCategory(
        Guid id,
        string name,
        Guid? tenantId = null,
        Guid? parentId = null,
        int displayOrder = 0)
        : base(id)
    {
        TenantId = tenantId;
        ParentId = parentId;
        DisplayOrder = displayOrder;
        IsActive = true;
        SetName(name);
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
