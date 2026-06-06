using System;
using System.Threading.Tasks;
using MediatR;
using Wesaya.Menu.Items.Commands;
using Wesaya.Menu.Items.Queries;
using Volo.Abp.Application.Dtos;
using Wesaya.Menu.Dtos;
using Wesaya.Menu.Services;

namespace Wesaya.Menu;

public class MenuItemAppService(ISender sender)
    : WesayaAppService, IMenuItemAppService
{
    public async Task<MenuItemDto> GetAsync(Guid id)
    {
        return await sender.Send(new GetMenuItemQuery(id));
    }

    public async Task<PagedResultDto<MenuItemDto>> GetListAsync(PagedAndSortedResultRequestDto input)
    {
        return await sender.Send(new GetMenuItemListQuery(input));
    }

    public async Task<MenuItemDto> CreateAsync(CreateUpdateMenuItemDto input)
    {
        return await sender.Send(new CreateMenuItemCommand(input));
    }

    public async Task<MenuItemDto> UpdateAsync(Guid id, CreateUpdateMenuItemDto input)
    {
        return await sender.Send(new UpdateMenuItemCommand(id, input));
    }

    public async Task DeleteAsync(Guid id)
    {
        await sender.Send(new DeleteMenuItemCommand(id));
    }

    public async Task<MenuItemDto> AddExtraItemAsync(Guid id, CreateUpdateExtraItemDto input)
    {
        return await sender.Send(new AddExtraItemToMenuItemCommand(id, input));
    }

    public async Task<MenuItemDto> RemoveExtraItemAsync(Guid id, string extraItemName)
    {
        return await sender.Send(new RemoveExtraItemFromMenuItemCommand(id, extraItemName));
    }
}
