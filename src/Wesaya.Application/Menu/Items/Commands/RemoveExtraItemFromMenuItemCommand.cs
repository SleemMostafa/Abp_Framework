using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;

namespace Wesaya.Menu.Items.Commands;

public record RemoveExtraItemFromMenuItemCommand(Guid Id, string ExtraItemName)
    : IRequest<MenuItemDto>;

public class RemoveExtraItemFromMenuItemCommandHandler(IRepository<MenuItem, Guid> menuItemRepository)
    : IRequestHandler<RemoveExtraItemFromMenuItemCommand, MenuItemDto>
{
    public async Task<MenuItemDto> Handle(
        RemoveExtraItemFromMenuItemCommand request,
        CancellationToken cancellationToken)
    {
        Check.NotDefaultOrNull<Guid>(request.Id, nameof(request.Id));
        Check.NotNullOrWhiteSpace(request.ExtraItemName, nameof(request.ExtraItemName), MenuConsts.MaxExtraItemNameLength);

        var item = await menuItemRepository.GetAsync(
            request.Id,
            cancellationToken: cancellationToken);

        item.RemoveExtraItem(request.ExtraItemName);

        await menuItemRepository.UpdateAsync(item, cancellationToken: cancellationToken);

        return MenuItemDtoMapper.ToDto(item);
    }
}
