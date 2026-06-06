using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Wesaya.Menu.Dtos;

namespace Wesaya.Menu.Items.Commands;

public record UpdateMenuItemCommand(Guid Id, CreateUpdateMenuItemDto Input)
    : IRequest<MenuItemDto>;

public class UpdateMenuItemCommandHandler(
    IRepository<MenuItem, Guid> menuItemRepository,
    IRepository<MenuCategory, Guid> categoryRepository)
    : IRequestHandler<UpdateMenuItemCommand, MenuItemDto>
{
    public async Task<MenuItemDto> Handle(
        UpdateMenuItemCommand request,
        CancellationToken cancellationToken)
    {
        Validate(request);

        await categoryRepository.GetAsync(
            request.Input.CategoryId,
            cancellationToken: cancellationToken);

        var item = await menuItemRepository.GetAsync(
            request.Id,
            cancellationToken: cancellationToken);

        item.Update(
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

        item.ClearExtraItems();
        foreach (var extraItem in request.Input.ExtraItems)
        {
            item.AddExtraItem(
                LocalizedStringFactory.CreateStrong(
                    extraItem.Name,
                    MenuConsts.MaxExtraItemNameLength),
                extraItem.Price);
        }

        await menuItemRepository.UpdateAsync(item, cancellationToken: cancellationToken);

        return MenuItemDtoMapper.ToDto(item);
    }

    private static void Validate(UpdateMenuItemCommand request)
    {
        Check.NotDefaultOrNull<Guid>(request.Id, nameof(request.Id));
        Check.NotNull(request.Input, nameof(request.Input));
        Check.NotDefaultOrNull<Guid>(request.Input.CategoryId, nameof(request.Input.CategoryId));
        Check.NotNull(request.Input.Name, nameof(request.Input.Name));

        if (request.Input.Price < 0)
        {
            throw new BusinessException("Wesaya:MenuItemPriceCannotBeNegative");
        }

        if (request.Input.PreparationTimeMinutes < 0)
        {
            throw new BusinessException("Wesaya:PreparationTimeCannotBeNegative");
        }
    }
}
