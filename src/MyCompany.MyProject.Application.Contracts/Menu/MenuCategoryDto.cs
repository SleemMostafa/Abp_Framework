using System;
using Volo.Abp.Application.Dtos;

namespace MyCompany.MyProject.Menu;

public class MenuCategoryDto : AuditedEntityDto<Guid>
{
    public string Name { get; set; } = string.Empty;

    public int DisplayOrder { get; set; }

    public bool IsActive { get; set; }
}
