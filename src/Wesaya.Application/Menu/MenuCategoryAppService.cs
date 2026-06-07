using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Wesaya.Menu.Categories;
using Wesaya.Menu.Categories.Commands;
using Wesaya.Menu.Categories.Queries;
using Wesaya.Permissions;

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

    [Authorize(WesayaPermissions.MenuCategories.Update)]
    public async Task<MenuCategoryDto> UpdateAsync(Guid id, CreateUpdateMenuCategoryDto input)
    {
        return await sender.Send(new UpdateMenuCategoryCommand(id, input));
    }

    [Authorize(WesayaPermissions.MenuCategories.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        await sender.Send(new DeleteMenuCategoryCommand(id));
    }
}
