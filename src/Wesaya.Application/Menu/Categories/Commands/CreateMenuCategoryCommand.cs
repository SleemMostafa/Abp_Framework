using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Localization;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Wesaya.Localization;
using Wesaya.Menu.Categories;

namespace Wesaya.Menu.Categories.Commands;

public record CreateMenuCategoryCommand(CreateUpdateMenuCategoryDto Input)
    : IRequest<MenuCategoryDto>;

public class CreateMenuCategoryCommandValidator : AbstractValidator<CreateMenuCategoryCommand>
{
    public CreateMenuCategoryCommandValidator(IStringLocalizer<WesayaResource> localizer)
    {
        RuleFor(x => x.Input)
            .NotNull()
            .WithMessage(localizer["MenuCategory:RequestRequired"]);

        When(x => x.Input != null, () =>
        {
            RuleFor(x => x.Input.Name)
                .NotNull()
                .WithMessage(localizer["MenuCategory:NameRequired"]);

            When(x => x.Input.Name != null, () =>
            {
                RuleFor(x => x.Input.Name!.English)
                    .NotEmpty()
                    .WithMessage(localizer["MenuCategory:EnglishNameRequired"])
                    .MaximumLength(MenuConsts.MaxCategoryNameLength)
                    .WithMessage(localizer["MenuCategory:NameMaxLength", MenuConsts.MaxCategoryNameLength]);

                RuleFor(x => x.Input.Name!.Arabic)
                    .NotEmpty()
                    .WithMessage(localizer["MenuCategory:ArabicNameRequired"])
                    .MaximumLength(MenuConsts.MaxCategoryNameLength)
                    .WithMessage(localizer["MenuCategory:NameMaxLength", MenuConsts.MaxCategoryNameLength]);
            });

            RuleFor(x => x.Input.DisplayOrder)
                .GreaterThanOrEqualTo(0)
                .WithMessage(localizer["MenuCategory:DisplayOrderMustBePositive"]);
        });
    }
}

public class CreateMenuCategoryCommandHandler(
    IRepository<MenuCategory, Guid> categoryRepository,
    IGuidGenerator guidGenerator)
    : IRequestHandler<CreateMenuCategoryCommand, MenuCategoryDto>
{
    public async Task<MenuCategoryDto> Handle(
        CreateMenuCategoryCommand request,
        CancellationToken cancellationToken)
    {
        var category = MenuCategory.Create(
            guidGenerator.Create(),
            LocalizedStringFactory.CreateStrong(
                request.Input.Name!,
                MenuConsts.MaxCategoryNameLength),
            request.Input.DisplayOrder,
            request.Input.IsActive);

        await categoryRepository.InsertAsync(category, cancellationToken: cancellationToken);

        return MenuCategoryDtoMapper.ToDto(category);
    }
}
