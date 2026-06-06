using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Volo.Abp.Domain.Repositories;
using Wesaya.Menu.Items;

namespace Wesaya.Menu.Items.Commands;

public record AddExtraItemToMenuItemCommand(Guid Id, CreateUpdateExtraItemDto Input)
    : IRequest<MenuItemDto>;

public class AddExtraItemToMenuItemCommandValidator : AbstractValidator<AddExtraItemToMenuItemCommand>
{
    public AddExtraItemToMenuItemCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.Input)
            .NotNull();

        When(x => x.Input != null, () =>
        {
            RuleFor(x => x.Input)
                .SetValidator(new CreateUpdateExtraItemDtoValidator());
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
        var item = await menuItemRepository.GetAsync(
            request.Id,
            cancellationToken: cancellationToken);

        item.AddExtraItem(
            LocalizedStringFactory.CreateStrong(
                request.Input.Name,
                MenuConsts.MaxExtraItemNameLength),
            request.Input.Price);

        await menuItemRepository.UpdateAsync(item, cancellationToken: cancellationToken);

        return MenuItemDtoMapper.ToDto(item);
    }
}
