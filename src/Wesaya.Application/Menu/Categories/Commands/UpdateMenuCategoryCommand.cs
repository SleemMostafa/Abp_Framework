using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Localization;
using Volo.Abp.Domain.Repositories;
using Wesaya.Localization;
using Wesaya.Menu.Exceptions;

namespace Wesaya.Menu.Categories.Commands;

public record UpdateMenuCategoryCommand(Guid Id, CreateUpdateMenuCategoryDto Input)
    : IRequest<MenuCategoryDto>;

public class UpdateMenuCategoryCommandValidator : AbstractValidator<UpdateMenuCategoryCommand>
{
    public UpdateMenuCategoryCommandValidator(IStringLocalizer<WesayaResource> localizer)
    {
        RuleFor(x => x.Id)
            .NotEmpty();

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

public class UpdateMenuCategoryCommandHandler(IRepository<MenuCategory, Guid> categoryRepository)
    : IRequestHandler<UpdateMenuCategoryCommand, MenuCategoryDto>
{
    public async Task<MenuCategoryDto> Handle(
        UpdateMenuCategoryCommand request,
        CancellationToken cancellationToken)
    {
        var category = await categoryRepository.FindAsync(
            request.Id,
            cancellationToken: cancellationToken)
            ?? throw new MenuCategoryNotFoundException();

        category.Update(
            LocalizedStringFactory.CreateStrong(
                request.Input.Name!,
                MenuConsts.MaxCategoryNameLength),
            request.Input.DisplayOrder,
            request.Input.IsActive);

        await categoryRepository.UpdateAsync(category, cancellationToken: cancellationToken);

        return MenuCategoryDtoMapper.ToDto(category);
    }
}
