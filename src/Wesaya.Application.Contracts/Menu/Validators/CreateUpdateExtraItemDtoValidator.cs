using FluentValidation;
using Microsoft.Extensions.Localization;
using Wesaya.Localization;
using Wesaya.Menu.Dtos;

namespace Wesaya.Menu.Validators;

public class CreateUpdateExtraItemDtoValidator : AbstractValidator<CreateUpdateExtraItemDto>
{
    public CreateUpdateExtraItemDtoValidator(IStringLocalizer<WesayaResource> localizer)
    {
        RuleFor(x => x.Name)
            .NotNull();

        RuleFor(x => x.Name.English)
            .NotEmpty()
            .MaximumLength(MenuConsts.MaxExtraItemNameLength);

        RuleFor(x => x.Name.Arabic)
            .NotEmpty()
            .MaximumLength(MenuConsts.MaxExtraItemNameLength);

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0);
    }
}
