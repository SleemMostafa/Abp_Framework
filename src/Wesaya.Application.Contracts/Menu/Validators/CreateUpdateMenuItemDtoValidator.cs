using FluentValidation;
using Microsoft.Extensions.Localization;
using Wesaya.Localization;
using Wesaya.Menu.Dtos;

namespace Wesaya.Menu.Validators;

public class CreateUpdateMenuItemDtoValidator : AbstractValidator<CreateUpdateMenuItemDto>
{
    public CreateUpdateMenuItemDtoValidator(IStringLocalizer<WesayaResource> localizer)
    {
        RuleFor(x => x.CategoryId)
            .NotEmpty();

        RuleFor(x => x.Name)
            .NotNull();

        RuleFor(x => x.Name.English)
            .NotEmpty()
            .MaximumLength(MenuConsts.MaxItemNameLength);

        RuleFor(x => x.Name.Arabic)
            .NotEmpty()
            .MaximumLength(MenuConsts.MaxItemNameLength);

        When(x => x.Description != null, () =>
        {
            RuleFor(x => x.Description!.English)
                .MaximumLength(MenuConsts.MaxItemDescriptionLength);

            RuleFor(x => x.Description!.Arabic)
                .MaximumLength(MenuConsts.MaxItemDescriptionLength);
        });

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.PreparationTimeMinutes)
            .GreaterThanOrEqualTo(0);

        RuleForEach(x => x.ExtraItems)
            .SetValidator(new CreateUpdateExtraItemDtoValidator(localizer));
    }
}
