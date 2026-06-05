using System;
using System.Collections.Generic;

namespace Wesaya.Menu;

public class CreateUpdateMenuItemDto
{
    public Guid CategoryId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public bool IsAvailable { get; set; } = true;

    public int PreparationTimeMinutes { get; set; }

    public List<CreateUpdateExtraItemDto> ExtraItems { get; set; } = [];
}
