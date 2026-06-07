using Wesaya.Menu.Shared;

namespace Wesaya.Menu.Categories;

public sealed class CreateUpdateMenuCategoryDto
{
    public StrongLocalizedStringInputDto? Name { get; init; }

    public int DisplayOrder { get; init; }

    public bool IsActive { get; init; } = true;
}
