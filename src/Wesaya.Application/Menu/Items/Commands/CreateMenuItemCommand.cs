using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;

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
            request.Input.Name,
            request.Input.Price,
            request.Input.Description,
            request.Input.PreparationTimeMinutes,
            request.Input.IsAvailable);

        foreach (var extraItem in request.Input.ExtraItems)
        {
            item.AddExtraItem(extraItem.Name, extraItem.Price);
        }

        await menuItemRepository.InsertAsync(item, cancellationToken: cancellationToken);

        return MenuItemDtoMapper.ToDto(item);
    }

    private static void Validate(CreateUpdateMenuItemDto input)
    {
        Check.NotNull(input, nameof(input));
        Check.NotDefaultOrNull<Guid>(input.CategoryId, nameof(input.CategoryId));
        Check.NotNullOrWhiteSpace(input.Name, nameof(input.Name), MenuConsts.MaxItemNameLength);
        Check.Length(input.Description, nameof(input.Description), MenuConsts.MaxItemDescriptionLength);

        if (input.Price < 0)
        {
            throw new BusinessException("Wesaya:MenuItemPriceCannotBeNegative");
        }

        if (input.PreparationTimeMinutes < 0)
        {
            throw new BusinessException("Wesaya:PreparationTimeCannotBeNegative");
        }
    }
}
