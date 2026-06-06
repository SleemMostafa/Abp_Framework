using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Wesaya.Menu.Items;

namespace Wesaya.Menu.Items.Queries;

public record GetMenuItemListQuery(PagedAndSortedResultRequestDto Input)
    : IRequest<PagedResultDto<MenuItemDto>>;

public class GetMenuItemListQueryValidator : AbstractValidator<GetMenuItemListQuery>
{
    public GetMenuItemListQueryValidator()
    {
        RuleFor(x => x.Input)
            .NotNull();

        When(x => x.Input != null, () =>
        {
            RuleFor(x => x.Input.SkipCount)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.Input.MaxResultCount)
                .InclusiveBetween(1, 1000);
        });
    }
}

public class GetMenuItemListQueryHandler(IRepository<MenuItem, Guid> menuItemRepository)
    : IRequestHandler<GetMenuItemListQuery, PagedResultDto<MenuItemDto>>
{
    public async Task<PagedResultDto<MenuItemDto>> Handle(
        GetMenuItemListQuery request,
        CancellationToken cancellationToken)
    {
        var input = request.Input;
        var queryable = await menuItemRepository.GetQueryableAsync();

        var totalCount = queryable.Count();
        var items = queryable
            .OrderBy(x => x.Name.English)
            .Skip(input.SkipCount)
            .Take(input.MaxResultCount)
            .ToList();

        return new PagedResultDto<MenuItemDto>(
            totalCount,
            items.Select(MenuItemDtoMapper.ToDto).ToList());
    }
}
