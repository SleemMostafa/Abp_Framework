using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Wesaya.Menu.Categories;

namespace Wesaya.Menu.Categories.Queries;

public record GetMenuCategoryListQuery(PagedAndSortedResultRequestDto Input)
    : IRequest<PagedResultDto<MenuCategoryDto>>;

public class GetMenuCategoryListQueryHandler(IRepository<MenuCategory, Guid> categoryRepository)
    : IRequestHandler<GetMenuCategoryListQuery, PagedResultDto<MenuCategoryDto>>
{
    public async Task<PagedResultDto<MenuCategoryDto>> Handle(
        GetMenuCategoryListQuery request,
        CancellationToken cancellationToken)
    {
        var input = request.Input;
        var queryable = await categoryRepository.GetQueryableAsync();

        var totalCount = queryable.Count();
        var categories = queryable
            .OrderBy(x => x.DisplayOrder)
            .ThenBy(x => x.Name.English)
            .Skip(input.SkipCount)
            .Take(input.MaxResultCount)
            .ToList();

        return new PagedResultDto<MenuCategoryDto>(
            totalCount,
            categories.Select(MenuCategoryDtoMapper.ToDto).ToList());
    }
}
