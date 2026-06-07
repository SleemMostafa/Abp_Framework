using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Localization;
using Volo.Abp.Domain.Repositories;
using Wesaya.Localization;
using Wesaya.Menu.Exceptions;

namespace Wesaya.Menu.Items.Commands;

public record RemoveExtraItemFromMenuItemCommand(Guid Id, string ExtraItemName)
    : IRequest<MenuItemDto>;

public class RemoveExtraItemFromMenuItemCommandValidator : AbstractValidator<RemoveExtraItemFromMenuItemCommand>
{
    public RemoveExtraItemFromMenuItemCommandValidator(IStringLocalizer<WesayaResource> localizer)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(localizer["MenuItem:IdRequired"]);

        RuleFor(x => x.ExtraItemName)
            .NotEmpty()
            .WithMessage(localizer["ExtraItem:NameRequired"])
            .MaximumLength(MenuConsts.MaxExtraItemNameLength)
            .WithMessage(localizer["ExtraItem:NameMaxLength", MenuConsts.MaxExtraItemNameLength]);
    }
}

public class RemoveExtraItemFromMenuItemCommandHandler(IRepository<MenuItem, Guid> menuItemRepository)
    : IRequestHandler<RemoveExtraItemFromMenuItemCommand, MenuItemDto>
{
    public async Task<MenuItemDto> Handle(
        RemoveExtraItemFromMenuItemCommand request,
        CancellationToken cancellationToken)
    {
        var item = await menuItemRepository.FindAsync(
            request.Id,
            cancellationToken: cancellationToken)
            ?? throw new MenuItemNotFoundException();

        item.RemoveExtraItem(request.ExtraItemName);

        await menuItemRepository.UpdateAsync(item, cancellationToken: cancellationToken);

        return MenuItemDtoMapper.ToDto(item);
    }
}
