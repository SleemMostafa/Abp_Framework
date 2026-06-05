using System;
using System.Threading.Tasks;
using MediatR;
using Wesaya.Menu.Categories.Commands;
using Wesaya.Menu.Categories.Queries;
using Volo.Abp.Application.Dtos;

namespace Wesaya.Menu;

public class MenuCategoryAppService(ISender sender)
    : WesayaAppService, IMenuCategoryAppService
{
    public async Task<MenuCategoryDto> GetAsync(Guid id)
    {
        return await sender.Send(new GetMenuCategoryQuery(id));
    }

    public async Task<PagedResultDto<MenuCategoryDto>> GetListAsync(PagedAndSortedResultRequestDto input)
    {
        return await sender.Send(new GetMenuCategoryListQuery(input));
    }

    public async Task<MenuCategoryDto> CreateAsync(CreateUpdateMenuCategoryDto input)
    {
        return await sender.Send(new CreateMenuCategoryCommand(input));
    }

    public async Task<MenuCategoryDto> UpdateAsync(Guid id, CreateUpdateMenuCategoryDto input)
    {
        return await sender.Send(new UpdateMenuCategoryCommand(id, input));
    }

    public async Task DeleteAsync(Guid id)
    {
        await sender.Send(new DeleteMenuCategoryCommand(id));
    }
}
