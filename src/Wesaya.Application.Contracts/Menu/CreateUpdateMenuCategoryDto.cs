namespace Wesaya.Menu;

public sealed class CreateUpdateMenuCategoryDto
{
    public required string Name { get; init; } 

    public required int DisplayOrder { get; init; }

    public required bool IsActive { get; init; } = true;
}
