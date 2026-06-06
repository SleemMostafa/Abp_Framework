using Wesaya.Menu.Shared;

namespace Wesaya.Menu.Categories;

public sealed class CreateUpdateMenuCategoryDto
{
    public required StrongLocalizedStringInputDto Name { get; init; }

    public required int DisplayOrder { get; init; }

    public required bool IsActive { get; init; } = true;
}
