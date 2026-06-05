using FluentValidation;
using Microsoft.Extensions.Localization;
using Wesaya.Localization;

namespace Wesaya.Menu;

public class CreateUpdateExtraItemDtoValidator : AbstractValidator<CreateUpdateExtraItemDto>
{
    public CreateUpdateExtraItemDtoValidator(IStringLocalizer<WesayaResource> localizer)
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(MenuConsts.MaxExtraItemNameLength);

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0);
    }
}
