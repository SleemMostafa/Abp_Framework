using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Wesaya.Menu.Items;

public sealed class MenuItemDto : AuditedEntityDto<Guid>
{
    public Guid CategoryId { get; init; }

    public string Name { get; init; } = string.Empty;

    public string? Description { get; init; }

    public decimal Price { get; init; }

    public bool IsAvailable { get; init; }

    public int PreparationTimeMinutes { get; init; }

    public List<ExtraItemDto> ExtraItems { get; init; } = [];
}
