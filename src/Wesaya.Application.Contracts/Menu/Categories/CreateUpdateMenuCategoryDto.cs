using Wesaya.Menu.Shared;

namespace Wesaya.Menu.Categories;

public sealed class CreateUpdateMenuCategoryDto
{
    public StrongLocalizedStringInputDto? Name { get; set; }

    public int DisplayOrder { get; set; }

    public bool IsActive { get; set; } = true;
}
