using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Wesaya.Menu.Items;

namespace Wesaya.Menu.Items.Queries;

public record GetMenuItemQuery(Guid Id) : IRequest<MenuItemDto>;

public class GetMenuItemQueryHandler(IRepository<MenuItem, Guid> menuItemRepository)
    : IRequestHandler<GetMenuItemQuery, MenuItemDto>
{
    public async Task<MenuItemDto> Handle(
        GetMenuItemQuery request,
        CancellationToken cancellationToken)
    {
        Check.NotDefaultOrNull<Guid>(request.Id, nameof(request.Id));

        var item = await menuItemRepository.GetAsync(
            request.Id,
            cancellationToken: cancellationToken);

        return MenuItemDtoMapper.ToDto(item);
    }
}
