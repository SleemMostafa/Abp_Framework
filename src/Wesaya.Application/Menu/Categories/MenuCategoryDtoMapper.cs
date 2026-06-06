using Wesaya.Menu.Categories;

namespace Wesaya.Menu.Categories;

internal static class MenuCategoryDtoMapper
{
    public static MenuCategoryDto ToDto(MenuCategory category)
    {
        return new MenuCategoryDto
        {
            Id = category.Id,
            Name = category.Name.GetValue(),
            DisplayOrder = category.DisplayOrder,
            IsActive = category.IsActive,
            CreationTime = category.CreationTime,
            CreatorId = category.CreatorId,
            LastModificationTime = category.LastModificationTime,
            LastModifierId = category.LastModifierId
        };
    }
}
