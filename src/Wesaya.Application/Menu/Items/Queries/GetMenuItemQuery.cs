using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Localization;
using Volo.Abp.Domain.Repositories;
using Wesaya.Localization;
using Wesaya.Menu.Exceptions;

namespace Wesaya.Menu.Items.Queries;

public record GetMenuItemQuery(Guid Id) : IRequest<MenuItemDto>;

public class GetMenuItemQueryValidator : AbstractValidator<GetMenuItemQuery>
{
    public GetMenuItemQueryValidator(IStringLocalizer<WesayaResource> localizer)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(localizer["MenuItem:IdRequired"]);
    }
}

public class GetMenuItemQueryHandler(IRepository<MenuItem, Guid> menuItemRepository)
    : IRequestHandler<GetMenuItemQuery, MenuItemDto>
{
    public async Task<MenuItemDto> Handle(
        GetMenuItemQuery request,
        CancellationToken cancellationToken)
    {
        var item = await menuItemRepository.FindAsync(
            request.Id,
            cancellationToken: cancellationToken)
            ?? throw new MenuItemNotFoundException();

        return MenuItemDtoMapper.ToDto(item);
    }
}
