using FluentValidation;
using Microsoft.Extensions.Localization;
using Wesaya.Localization;

namespace Wesaya.Menu;

public class CreateUpdateMenuItemDtoValidator : AbstractValidator<CreateUpdateMenuItemDto>
{
    public CreateUpdateMenuItemDtoValidator(IStringLocalizer<WesayaResource> localizer)
    {
        RuleFor(x => x.CategoryId)
            .NotEmpty();

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(MenuConsts.MaxItemNameLength);

        RuleFor(x => x.Description)
            .MaximumLength(MenuConsts.MaxItemDescriptionLength);

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.PreparationTimeMinutes)
            .GreaterThanOrEqualTo(0);

        RuleForEach(x => x.ExtraItems)
            .SetValidator(new CreateUpdateExtraItemDtoValidator(localizer));
    }
}
