using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Wesaya.Menu.Items;

public class MenuItemDto : AuditedEntityDto<Guid>
{
    public Guid CategoryId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public bool IsAvailable { get; set; }

    public int PreparationTimeMinutes { get; set; }

    public List<ExtraItemDto> ExtraItems { get; set; } = [];
}
