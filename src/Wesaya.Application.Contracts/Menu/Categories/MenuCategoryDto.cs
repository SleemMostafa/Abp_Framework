using System;
using Volo.Abp.Application.Dtos;

namespace Wesaya.Menu.Categories;

public class MenuCategoryDto : AuditedEntityDto<Guid>
{
    public required string Name { get; init; }

    public required int DisplayOrder { get; init; }

    public required bool IsActive { get; init; }
}
