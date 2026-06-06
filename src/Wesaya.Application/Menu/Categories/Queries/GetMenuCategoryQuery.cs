using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Volo.Abp.Domain.Repositories;
using Wesaya.Menu.Categories;

namespace Wesaya.Menu.Categories.Queries;

public record GetMenuCategoryQuery(Guid Id) : IRequest<MenuCategoryDto>;

public class GetMenuCategoryQueryValidator : AbstractValidator<GetMenuCategoryQuery>
{
    public GetMenuCategoryQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}

public class GetMenuCategoryQueryHandler(IRepository<MenuCategory, Guid> categoryRepository)
    : IRequestHandler<GetMenuCategoryQuery, MenuCategoryDto>
{
    public async Task<MenuCategoryDto> Handle(
        GetMenuCategoryQuery request,
        CancellationToken cancellationToken)
    {
        var category = await categoryRepository.GetAsync(
            request.Id,
            cancellationToken: cancellationToken);

        return MenuCategoryDtoMapper.ToDto(category);
    }
}
