using System.Linq;
using Wesaya.Menu.Dtos;

namespace Wesaya.Menu.Items;

internal static class MenuItemDtoMapper
{
    public static MenuItemDto ToDto(MenuItem item)
    {
        return new MenuItemDto
        {
            Id = item.Id,
            CreationTime = item.CreationTime,
            CreatorId = item.CreatorId,
            LastModificationTime = item.LastModificationTime,
            LastModifierId = item.LastModifierId,
            CategoryId = item.CategoryId,
            Name = item.Name.GetValue(),
            Description = item.Description.GetValue(),
            Price = item.Price,
            IsAvailable = item.IsAvailable,
            PreparationTimeMinutes = item.PreparationTimeMinutes,
            ExtraItems = item.ExtraItems
                .Select(extraItem => new ExtraItemDto
                {
                    Name = extraItem.Name.GetValue(),
                    Price = extraItem.Price
                })
                .ToList()
        };
    }
}
