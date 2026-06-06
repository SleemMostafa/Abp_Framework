namespace Wesaya.Menu.Dtos;

public sealed class CreateUpdateMenuCategoryDto
{
    public required StrongLocalizedStringInputDto Name { get; init; }

    public required int DisplayOrder { get; init; }

    public required bool IsActive { get; init; } = true;
}
