using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Volo.Abp.Domain.Repositories;
using Wesaya.Menu.Items;

namespace Wesaya.Menu.Items.Queries;

public record GetMenuItemQuery(Guid Id) : IRequest<MenuItemDto>;

public class GetMenuItemQueryValidator : AbstractValidator<GetMenuItemQuery>
{
    public GetMenuItemQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}

public class GetMenuItemQueryHandler(IRepository<MenuItem, Guid> menuItemRepository)
    : IRequestHandler<GetMenuItemQuery, MenuItemDto>
{
    public async Task<MenuItemDto> Handle(
        GetMenuItemQuery request,
        CancellationToken cancellationToken)
    {
        var item = await menuItemRepository.GetAsync(
            request.Id,
            cancellationToken: cancellationToken);

        return MenuItemDtoMapper.ToDto(item);
    }
}
