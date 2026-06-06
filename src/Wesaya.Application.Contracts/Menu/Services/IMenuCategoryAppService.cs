using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Wesaya.Menu.Dtos;

namespace Wesaya.Menu.Services;

public interface IMenuCategoryAppService : IApplicationService
{
    Task<MenuCategoryDto> GetAsync(Guid id);

    Task<PagedResultDto<MenuCategoryDto>> GetListAsync(PagedAndSortedResultRequestDto input);

    Task<MenuCategoryDto> CreateAsync(CreateUpdateMenuCategoryDto input);

    Task<MenuCategoryDto> UpdateAsync(Guid id, CreateUpdateMenuCategoryDto input);

    Task DeleteAsync(Guid id);
}
