using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Localization;
using Volo.Abp.Domain.Repositories;
using Wesaya.Localization;
using Wesaya.Menu.Exceptions;
using Wesaya.Menu.Items;

namespace Wesaya.Menu.Items.Commands;

public record AddExtraItemToMenuItemCommand(Guid Id, CreateUpdateExtraItemDto Input)
    : IRequest<MenuItemDto>;

public class AddExtraItemToMenuItemCommandValidator : AbstractValidator<AddExtraItemToMenuItemCommand>
{
    public AddExtraItemToMenuItemCommandValidator(IStringLocalizer<WesayaResource> localizer)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(localizer["MenuItem:IdRequired"]);

        RuleFor(x => x.Input)
            .NotNull()
            .WithMessage(localizer["ExtraItem:RequestRequired"]);

        When(x => x.Input != null, () =>
        {
            RuleFor(x => x.Input)
                .SetValidator(new CreateUpdateExtraItemDtoValidator(localizer));
        });
    }
}

public class AddExtraItemToMenuItemCommandHandler(IRepository<MenuItem, Guid> menuItemRepository)
    : IRequestHandler<AddExtraItemToMenuItemCommand, MenuItemDto>
{
    public async Task<MenuItemDto> Handle(
        AddExtraItemToMenuItemCommand request,
        CancellationToken cancellationToken)
    {
        var item = await menuItemRepository.FindAsync(
            request.Id,
            cancellationToken: cancellationToken)
            ?? throw new MenuItemNotFoundException();

        item.AddExtraItem(
            LocalizedStringFactory.CreateStrong(
                request.Input.Name,
                MenuConsts.MaxExtraItemNameLength),
            request.Input.Price);

        await menuItemRepository.UpdateAsync(item, cancellationToken: cancellationToken);

        return MenuItemDtoMapper.ToDto(item);
    }
}
