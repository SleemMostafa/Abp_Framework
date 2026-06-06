using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Volo.Abp.Domain.Repositories;
using Wesaya.Menu.Categories;

namespace Wesaya.Menu.Categories.Commands;

public record UpdateMenuCategoryCommand(Guid Id, CreateUpdateMenuCategoryDto Input)
    : IRequest<MenuCategoryDto>;

public class UpdateMenuCategoryCommandValidator : AbstractValidator<UpdateMenuCategoryCommand>
{
    public UpdateMenuCategoryCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.Input)
            .NotNull();

        When(x => x.Input != null, () =>
        {
            RuleFor(x => x.Input.Name)
                .NotNull();

            When(x => x.Input.Name != null, () =>
            {
                RuleFor(x => x.Input.Name.English)
                    .NotEmpty()
                    .MaximumLength(MenuConsts.MaxCategoryNameLength);

                RuleFor(x => x.Input.Name.Arabic)
                    .NotEmpty()
                    .MaximumLength(MenuConsts.MaxCategoryNameLength);
            });

            RuleFor(x => x.Input.DisplayOrder)
                .GreaterThanOrEqualTo(0);
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
        var category = await categoryRepository.GetAsync(
            request.Id,
            cancellationToken: cancellationToken);

        category.Update(
            LocalizedStringFactory.CreateStrong(
                request.Input.Name,
                MenuConsts.MaxCategoryNameLength),
            request.Input.DisplayOrder,
            request.Input.IsActive);

        await categoryRepository.UpdateAsync(category, cancellationToken: cancellationToken);

        return MenuCategoryDtoMapper.ToDto(category);
    }
}
