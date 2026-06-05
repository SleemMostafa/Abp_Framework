namespace Wesaya.Menu;

public sealed class CreateUpdateMenuCategoryDto
{
    public string Name { get; set; } = string.Empty;

    public int DisplayOrder { get; set; }

    public bool IsActive { get; set; } = true;
}
