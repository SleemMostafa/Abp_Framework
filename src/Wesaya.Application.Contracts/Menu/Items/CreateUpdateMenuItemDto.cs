using System;
using System.Collections.Generic;
using Wesaya.Menu.Shared;

namespace Wesaya.Menu.Items;

public sealed class CreateUpdateMenuItemDto
{
    public Guid CategoryId { get; init; }

    public StrongLocalizedStringInputDto Name { get; init; } = new();

    public OptionalLocalizedStringInputDto? Description { get; init; }

    public decimal Price { get; init; }

    public bool IsAvailable { get; init; } = true;

    public int PreparationTimeMinutes { get; init; }

    public List<CreateUpdateExtraItemDto> ExtraItems { get; init; } = [];
}
