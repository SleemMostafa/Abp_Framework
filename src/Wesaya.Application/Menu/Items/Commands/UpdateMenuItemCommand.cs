using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;

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
            request.Input.Name,
            request.Input.Price,
            request.Input.Description,
            request.Input.PreparationTimeMinutes,
            request.Input.IsAvailable);

        item.ClearExtraItems();
        foreach (var extraItem in request.Input.ExtraItems)
        {
            item.AddExtraItem(extraItem.Name, extraItem.Price);
        }

        await menuItemRepository.UpdateAsync(item, cancellationToken: cancellationToken);

        return MenuItemDtoMapper.ToDto(item);
    }

    private static void Validate(UpdateMenuItemCommand request)
    {
        Check.NotDefaultOrNull<Guid>(request.Id, nameof(request.Id));
        Check.NotNull(request.Input, nameof(request.Input));
        Check.NotDefaultOrNull<Guid>(request.Input.CategoryId, nameof(request.Input.CategoryId));
        Check.NotNullOrWhiteSpace(request.Input.Name, nameof(request.Input.Name), MenuConsts.MaxItemNameLength);
        Check.Length(request.Input.Description, nameof(request.Input.Description), MenuConsts.MaxItemDescriptionLength);

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
