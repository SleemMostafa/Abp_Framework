using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Wesaya.Menu.Categories;

namespace Wesaya.Menu.Categories.Commands;

public record CreateMenuCategoryCommand(CreateUpdateMenuCategoryDto Input)
    : IRequest<MenuCategoryDto>;

public class CreateMenuCategoryCommandValidator : AbstractValidator<CreateMenuCategoryCommand>
{
    public CreateMenuCategoryCommandValidator()
    {
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
                request.Input.Name,
                MenuConsts.MaxCategoryNameLength),
            request.Input.DisplayOrder,
            request.Input.IsActive);

        await categoryRepository.InsertAsync(category, cancellationToken: cancellationToken);

        return MenuCategoryDtoMapper.ToDto(category);
    }
}
