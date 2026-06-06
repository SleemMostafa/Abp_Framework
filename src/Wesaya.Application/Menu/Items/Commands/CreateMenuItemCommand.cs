using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Wesaya.Menu.Items;
using Wesaya.Menu.Exceptions;

namespace Wesaya.Menu.Items.Commands;

public record CreateMenuItemCommand(CreateUpdateMenuItemDto Input)
    : IRequest<MenuItemDto>;

public class CreateMenuItemCommandHandler(
    IRepository<MenuItem, Guid> menuItemRepository,
    IRepository<MenuCategory, Guid> categoryRepository,
    IGuidGenerator guidGenerator)
    : IRequestHandler<CreateMenuItemCommand, MenuItemDto>
{
    public async Task<MenuItemDto> Handle(
        CreateMenuItemCommand request,
        CancellationToken cancellationToken)
    {
        Validate(request.Input);

        await categoryRepository.GetAsync(
            request.Input.CategoryId,
            cancellationToken: cancellationToken);

        var item = MenuItem.Create(
            guidGenerator.Create(),
            request.Input.CategoryId,
            LocalizedStringFactory.CreateStrong(
                request.Input.Name,
                MenuConsts.MaxItemNameLength),
            request.Input.Price,
            LocalizedStringFactory.CreateOptional(
                request.Input.Description,
                MenuConsts.MaxItemDescriptionLength),
            request.Input.PreparationTimeMinutes,
            request.Input.IsAvailable);

        foreach (var extraItem in request.Input.ExtraItems)
        {
            item.AddExtraItem(
                LocalizedStringFactory.CreateStrong(
                    extraItem.Name,
                    MenuConsts.MaxExtraItemNameLength),
                extraItem.Price);
        }

        await menuItemRepository.InsertAsync(item, cancellationToken: cancellationToken);

        return MenuItemDtoMapper.ToDto(item);
    }

    private static void Validate(CreateUpdateMenuItemDto input)
    {
        Check.NotNull(input, nameof(input));
        Check.NotDefaultOrNull<Guid>(input.CategoryId, nameof(input.CategoryId));
        Check.NotNull(input.Name, nameof(input.Name));

        if (input.Price < 0)
        {
            throw new MenuItemPriceCannotBeNegativeException();
        }

        if (input.PreparationTimeMinutes < 0)
        {
            throw new PreparationTimeCannotBeNegativeException();
        }
    }
}
