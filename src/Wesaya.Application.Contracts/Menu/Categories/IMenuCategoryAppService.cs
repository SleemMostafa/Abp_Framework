using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Wesaya.Menu.Categories;

public interface IMenuCategoryAppService : IApplicationService
{
    Task<MenuCategoryDto> GetAsync(Guid id);

    Task<PagedResultDto<MenuCategoryDto>> GetListAsync(PagedAndSortedResultRequestDto input);

    Task<MenuCategoryDto> CreateAsync(CreateUpdateMenuCategoryDto input);

    Task<MenuCategoryDto> UpdateAsync(Guid id, CreateUpdateMenuCategoryDto input);

    Task DeleteAsync(Guid id);
}
