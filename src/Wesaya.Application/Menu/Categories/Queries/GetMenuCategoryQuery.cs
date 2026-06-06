using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Wesaya.Menu.Dtos;

namespace Wesaya.Menu.Categories.Queries;

public record GetMenuCategoryQuery(Guid Id) : IRequest<MenuCategoryDto>;

public class GetMenuCategoryQueryHandler(IRepository<MenuCategory, Guid> categoryRepository)
    : IRequestHandler<GetMenuCategoryQuery, MenuCategoryDto>
{
    public async Task<MenuCategoryDto> Handle(
        GetMenuCategoryQuery request,
        CancellationToken cancellationToken)
    {
        Check.NotDefaultOrNull<Guid>(request.Id, nameof(request.Id));

        var category = await categoryRepository.GetAsync(
            request.Id,
            cancellationToken: cancellationToken);

        return MenuCategoryDtoMapper.ToDto(category);
    }
}
