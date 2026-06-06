using FluentValidation;
using Microsoft.Extensions.Localization;
using Wesaya.Localization;

namespace Wesaya.Menu.Categories;

public class CreateUpdateMenuCategoryDtoValidator : AbstractValidator<CreateUpdateMenuCategoryDto>
{
    public CreateUpdateMenuCategoryDtoValidator(IStringLocalizer<WesayaResource> localizer)
    {
        RuleFor(x => x.Name)
            .NotNull();

        RuleFor(x => x.Name.English)
            .NotEmpty()
            .MaximumLength(MenuConsts.MaxCategoryNameLength);

        RuleFor(x => x.Name.Arabic)
            .NotEmpty()
            .MaximumLength(MenuConsts.MaxCategoryNameLength);

        RuleFor(x => x.DisplayOrder)
            .GreaterThanOrEqualTo(0);
    }
}
