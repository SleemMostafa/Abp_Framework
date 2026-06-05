using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;

namespace Wesaya.Menu.Items.Commands;

public record AddExtraItemToMenuItemCommand(Guid Id, CreateUpdateExtraItemDto Input)
    : IRequest<MenuItemDto>;

public class AddExtraItemToMenuItemCommandHandler(IRepository<MenuItem, Guid> menuItemRepository)
    : IRequestHandler<AddExtraItemToMenuItemCommand, MenuItemDto>
{
    public async Task<MenuItemDto> Handle(
        AddExtraItemToMenuItemCommand request,
        CancellationToken cancellationToken)
    {
        Validate(request);

        var item = await menuItemRepository.GetAsync(
            request.Id,
            cancellationToken: cancellationToken);

        item.AddExtraItem(request.Input.Name, request.Input.Price);

        await menuItemRepository.UpdateAsync(item, cancellationToken: cancellationToken);

        return MenuItemDtoMapper.ToDto(item);
    }

    private static void Validate(AddExtraItemToMenuItemCommand request)
    {
        Check.NotDefaultOrNull<Guid>(request.Id, nameof(request.Id));
        Check.NotNull(request.Input, nameof(request.Input));
        Check.NotNullOrWhiteSpace(request.Input.Name, nameof(request.Input.Name), MenuConsts.MaxExtraItemNameLength);

        if (request.Input.Price < 0)
        {
            throw new BusinessException("Wesaya:ExtraItemPriceCannotBeNegative");
        }
    }
}
