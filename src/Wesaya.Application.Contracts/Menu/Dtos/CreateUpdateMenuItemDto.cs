using System;
using System.Collections.Generic;

namespace Wesaya.Menu.Dtos;

public class CreateUpdateMenuItemDto
{
    public Guid CategoryId { get; set; }

    public StrongLocalizedStringInputDto Name { get; set; } = new();

    public OptionalLocalizedStringInputDto? Description { get; set; }

    public decimal Price { get; set; }

    public bool IsAvailable { get; set; } = true;

    public int PreparationTimeMinutes { get; set; }

    public List<CreateUpdateExtraItemDto> ExtraItems { get; set; } = [];
}
