using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Volo.Abp.Domain.Repositories;
using Wesaya.Menu.Items;

namespace Wesaya.Menu.Items.Commands;

public record RemoveExtraItemFromMenuItemCommand(Guid Id, string ExtraItemName)
    : IRequest<MenuItemDto>;

public class RemoveExtraItemFromMenuItemCommandValidator : AbstractValidator<RemoveExtraItemFromMenuItemCommand>
{
    public RemoveExtraItemFromMenuItemCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.ExtraItemName)
            .NotEmpty()
            .MaximumLength(MenuConsts.MaxExtraItemNameLength);
    }
}

public class RemoveExtraItemFromMenuItemCommandHandler(IRepository<MenuItem, Guid> menuItemRepository)
    : IRequestHandler<RemoveExtraItemFromMenuItemCommand, MenuItemDto>
{
    public async Task<MenuItemDto> Handle(
        RemoveExtraItemFromMenuItemCommand request,
        CancellationToken cancellationToken)
    {
        var item = await menuItemRepository.GetAsync(
            request.Id,
            cancellationToken: cancellationToken);

        item.RemoveExtraItem(request.ExtraItemName);

        await menuItemRepository.UpdateAsync(item, cancellationToken: cancellationToken);

        return MenuItemDtoMapper.ToDto(item);
    }
}
