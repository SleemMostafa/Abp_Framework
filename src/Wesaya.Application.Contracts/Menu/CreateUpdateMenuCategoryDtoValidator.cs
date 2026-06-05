using FluentValidation;
using Microsoft.Extensions.Localization;
using Wesaya.Localization;

namespace Wesaya.Menu;

public class CreateUpdateMenuCategoryDtoValidator : AbstractValidator<CreateUpdateMenuCategoryDto>
{
    public CreateUpdateMenuCategoryDtoValidator(IStringLocalizer<WesayaResource> localizer)
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(localizer["MenuCategory:NameRequired"])
            .MaximumLength(MenuConsts.MaxCategoryNameLength)
            .WithMessage(localizer["MenuCategory:NameMaxLength", MenuConsts.MaxCategoryNameLength]);

        RuleFor(x => x.DisplayOrder)
            .GreaterThanOrEqualTo(0)
            .WithMessage(localizer["MenuCategory:DisplayOrderMustBePositive"]);
    }
}
