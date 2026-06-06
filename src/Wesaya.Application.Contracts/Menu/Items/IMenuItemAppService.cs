using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Wesaya.Menu.Items;

public interface IMenuItemAppService : IApplicationService
{
    Task<MenuItemDto> GetAsync(Guid id);

    Task<PagedResultDto<MenuItemDto>> GetListAsync(PagedAndSortedResultRequestDto input);

    Task<MenuItemDto> CreateAsync(CreateUpdateMenuItemDto input);

    Task<MenuItemDto> UpdateAsync(Guid id, CreateUpdateMenuItemDto input);

    Task DeleteAsync(Guid id);

    Task<MenuItemDto> AddExtraItemAsync(Guid id, CreateUpdateExtraItemDto input);

    Task<MenuItemDto> RemoveExtraItemAsync(Guid id, string extraItemName);
}
