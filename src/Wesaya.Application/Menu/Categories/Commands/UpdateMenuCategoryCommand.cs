using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Wesaya.Menu.Categories;

namespace Wesaya.Menu.Categories.Commands;

public record UpdateMenuCategoryCommand(Guid Id, CreateUpdateMenuCategoryDto Input)
    : IRequest<MenuCategoryDto>;

public class UpdateMenuCategoryCommandHandler(IRepository<MenuCategory, Guid> categoryRepository)
    : IRequestHandler<UpdateMenuCategoryCommand, MenuCategoryDto>
{
    public async Task<MenuCategoryDto> Handle(
        UpdateMenuCategoryCommand request,
        CancellationToken cancellationToken)
    {
        Validate(request);

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

    private static void Validate(UpdateMenuCategoryCommand request)
    {
        Check.NotDefaultOrNull<Guid>(request.Id, nameof(request.Id));
        Check.NotNull(request.Input, nameof(request.Input));
        Check.NotNull(request.Input.Name, nameof(request.Input.Name));
    }
}
