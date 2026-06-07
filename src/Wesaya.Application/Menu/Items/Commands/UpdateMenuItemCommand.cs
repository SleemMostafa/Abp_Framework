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

public record UpdateMenuItemCommand(Guid Id, CreateUpdateMenuItemDto Input)
    : IRequest<MenuItemDto>;

public class UpdateMenuItemCommandValidator : AbstractValidator<UpdateMenuItemCommand>
{
    public UpdateMenuItemCommandValidator(IStringLocalizer<WesayaResource> localizer)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(localizer["MenuItem:IdRequired"]);

        RuleFor(x => x.Input)
            .NotNull()
            .WithMessage(localizer["MenuItem:RequestRequired"]);

        When(x => x.Input != null, () =>
        {
            RuleFor(x => x.Input.CategoryId)
                .NotEmpty()
                .WithMessage(localizer["MenuItem:CategoryRequired"]);

            RuleFor(x => x.Input.Name)
                .NotNull()
                .WithMessage(localizer["MenuItem:NameRequired"]);

            When(x => x.Input.Name != null, () =>
            {
                RuleFor(x => x.Input.Name.English)
                    .NotEmpty()
                    .WithMessage(localizer["MenuItem:EnglishNameRequired"])
                    .MaximumLength(MenuConsts.MaxItemNameLength)
                    .WithMessage(localizer["MenuItem:NameMaxLength", MenuConsts.MaxItemNameLength]);

                RuleFor(x => x.Input.Name.Arabic)
                    .NotEmpty()
                    .WithMessage(localizer["MenuItem:ArabicNameRequired"])
                    .MaximumLength(MenuConsts.MaxItemNameLength)
                    .WithMessage(localizer["MenuItem:NameMaxLength", MenuConsts.MaxItemNameLength]);
            });

            When(x => x.Input.Description != null, () =>
            {
                RuleFor(x => x.Input.Description!.English)
                    .MaximumLength(MenuConsts.MaxItemDescriptionLength)
                    .WithMessage(localizer["MenuItem:DescriptionMaxLength", MenuConsts.MaxItemDescriptionLength]);

                RuleFor(x => x.Input.Description!.Arabic)
                    .MaximumLength(MenuConsts.MaxItemDescriptionLength)
                    .WithMessage(localizer["MenuItem:DescriptionMaxLength", MenuConsts.MaxItemDescriptionLength]);
            });

            RuleFor(x => x.Input.Price)
                .GreaterThanOrEqualTo(0)
                .WithMessage(localizer["MenuItem:PriceMustBePositive"]);

            RuleFor(x => x.Input.PreparationTimeMinutes)
                .GreaterThanOrEqualTo(0)
                .WithMessage(localizer["MenuItem:PreparationTimeMustBePositive"]);

            RuleFor(x => x.Input.ExtraItems)
                .NotNull()
                .WithMessage(localizer["MenuItem:ExtraItemsRequired"]);

            When(x => x.Input.ExtraItems != null, () =>
            {
                RuleForEach(x => x.Input.ExtraItems)
                    .SetValidator(new CreateUpdateExtraItemDtoValidator(localizer));
            });
        });
    }
}

public class UpdateMenuItemCommandHandler(
    IRepository<MenuItem, Guid> menuItemRepository,
    IRepository<MenuCategory, Guid> categoryRepository)
    : IRequestHandler<UpdateMenuItemCommand, MenuItemDto>
{
    public async Task<MenuItemDto> Handle(
        UpdateMenuItemCommand request,
        CancellationToken cancellationToken)
    {
        _ = await categoryRepository.FindAsync(
            request.Input.CategoryId,
            cancellationToken: cancellationToken)
            ?? throw new MenuCategoryNotFoundException();

        var item = await menuItemRepository.FindAsync(
            request.Id,
            cancellationToken: cancellationToken)
            ?? throw new MenuItemNotFoundException();

        item.Update(
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

        item.ClearExtraItems();
        foreach (var extraItem in request.Input.ExtraItems)
        {
            item.AddExtraItem(
                LocalizedStringFactory.CreateStrong(
                    extraItem.Name,
                    MenuConsts.MaxExtraItemNameLength),
                extraItem.Price);
        }

        await menuItemRepository.UpdateAsync(item, cancellationToken: cancellationToken);

        return MenuItemDtoMapper.ToDto(item);
    }
}
