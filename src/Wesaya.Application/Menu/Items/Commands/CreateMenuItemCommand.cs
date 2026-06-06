using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Wesaya.Menu.Items;

namespace Wesaya.Menu.Items.Commands;

public record CreateMenuItemCommand(CreateUpdateMenuItemDto Input)
    : IRequest<MenuItemDto>;

public class CreateMenuItemCommandValidator : AbstractValidator<CreateMenuItemCommand>
{
    public CreateMenuItemCommandValidator()
    {
        RuleFor(x => x.Input)
            .NotNull();

        When(x => x.Input != null, () =>
        {
            RuleFor(x => x.Input.CategoryId)
                .NotEmpty();

            RuleFor(x => x.Input.Name)
                .NotNull();

            When(x => x.Input.Name != null, () =>
            {
                RuleFor(x => x.Input.Name.English)
                    .NotEmpty()
                    .MaximumLength(MenuConsts.MaxItemNameLength);

                RuleFor(x => x.Input.Name.Arabic)
                    .NotEmpty()
                    .MaximumLength(MenuConsts.MaxItemNameLength);
            });

            When(x => x.Input.Description != null, () =>
            {
                RuleFor(x => x.Input.Description!.English)
                    .MaximumLength(MenuConsts.MaxItemDescriptionLength);

                RuleFor(x => x.Input.Description!.Arabic)
                    .MaximumLength(MenuConsts.MaxItemDescriptionLength);
            });

            RuleFor(x => x.Input.Price)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.Input.PreparationTimeMinutes)
                .GreaterThanOrEqualTo(0);

            When(x => x.Input.ExtraItems != null, () =>
            {
                RuleForEach(x => x.Input.ExtraItems)
                    .SetValidator(new CreateUpdateExtraItemDtoValidator());
            });
        });
    }
}

public class CreateUpdateExtraItemDtoValidator : AbstractValidator<CreateUpdateExtraItemDto>
{
    public CreateUpdateExtraItemDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotNull();

        When(x => x.Name != null, () =>
        {
            RuleFor(x => x.Name.English)
                .NotEmpty()
                .MaximumLength(MenuConsts.MaxExtraItemNameLength);

            RuleFor(x => x.Name.Arabic)
                .NotEmpty()
                .MaximumLength(MenuConsts.MaxExtraItemNameLength);
        });

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0);
    }
}

public class CreateMenuItemCommandHandler(
    IRepository<MenuItem, Guid> menuItemRepository,
    IRepository<MenuCategory, Guid> categoryRepository,
    IGuidGenerator guidGenerator)
    : IRequestHandler<CreateMenuItemCommand, MenuItemDto>
{
    public async Task<MenuItemDto> Handle(
        CreateMenuItemCommand request,
        CancellationToken cancellationToken)
    {
        await categoryRepository.GetAsync(
            request.Input.CategoryId,
            cancellationToken: cancellationToken);

        var item = MenuItem.Create(
            guidGenerator.Create(),
            request.Input.CategoryId,
            LocalizedStringFactory.CreateStrong(
                request.Input.Name,
                MenuConsts.MaxItemNameLength),
            request.Input.Price,
            LocalizedStringFactory.CreateOptional(
                request.Input.Description,
                MenuConsts.MaxItemDescriptionLength),
            request.Input.PreparationTimeMinutes,
            request.Input.IsAvailable);

        foreach (var extraItem in request.Input.ExtraItems)
        {
            item.AddExtraItem(
                LocalizedStringFactory.CreateStrong(
                    extraItem.Name,
                    MenuConsts.MaxExtraItemNameLength),
                extraItem.Price);
        }

        await menuItemRepository.InsertAsync(item, cancellationToken: cancellationToken);

        return MenuItemDtoMapper.ToDto(item);
    }
}
