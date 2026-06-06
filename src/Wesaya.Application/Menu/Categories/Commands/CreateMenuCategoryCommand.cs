using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Wesaya.Menu.Dtos;

namespace Wesaya.Menu.Categories.Commands;

public record CreateMenuCategoryCommand(CreateUpdateMenuCategoryDto Input)
    : IRequest<MenuCategoryDto>;

public class CreateMenuCategoryCommandHandler(
    IRepository<MenuCategory, Guid> categoryRepository,
    IGuidGenerator guidGenerator)
    : IRequestHandler<CreateMenuCategoryCommand, MenuCategoryDto>
{
    public async Task<MenuCategoryDto> Handle(
        CreateMenuCategoryCommand request,
        CancellationToken cancellationToken)
    {
        Validate(request.Input);

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

    private static void Validate(CreateUpdateMenuCategoryDto input)
    {
        Check.NotNull(input, nameof(input));
        Check.NotNull(input.Name, nameof(input.Name));
    }
}
