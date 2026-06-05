using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace MyCompany.MyProject.Menu;

public class MenuCategoryAppService(IRepository<MenuCategory, Guid> categoryRepository)
    : MyProjectAppService, IMenuCategoryAppService
{
    public async Task<MenuCategoryDto> GetAsync(Guid id)
    {
        var category = await categoryRepository.GetAsync(id);

        return ToDto(category);
    }

    public async Task<PagedResultDto<MenuCategoryDto>> GetListAsync(PagedAndSortedResultRequestDto input)
    {
        var queryable = await categoryRepository.GetQueryableAsync();

        var totalCount = queryable.Count();
        var categories = queryable
            .OrderBy(x => x.DisplayOrder)
            .ThenBy(x => x.Name)
            .Skip(input.SkipCount)
            .Take(input.MaxResultCount)
            .ToList();

        return new PagedResultDto<MenuCategoryDto>(
            totalCount,
            categories.Select(ToDto).ToList());
    }

    public async Task<MenuCategoryDto> CreateAsync(CreateUpdateMenuCategoryDto input)
    {
        var category = MenuCategory.Create(
            GuidGenerator.Create(),
            input.Name,
            input.DisplayOrder,
            input.IsActive);

        await categoryRepository.InsertAsync(category);

        return ToDto(category);
    }

    public async Task<MenuCategoryDto> UpdateAsync(Guid id, CreateUpdateMenuCategoryDto input)
    {
        var category = await categoryRepository.GetAsync(id);

        category.Update(
            input.Name,
            input.DisplayOrder,
            input.IsActive);

        await categoryRepository.UpdateAsync(category);

        return ToDto(category);
    }

    public async Task DeleteAsync(Guid id)
    {
        await categoryRepository.DeleteAsync(id);
    }

    private static MenuCategoryDto ToDto(MenuCategory category)
    {
        return new MenuCategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            DisplayOrder = category.DisplayOrder,
            IsActive = category.IsActive,
            CreationTime = category.CreationTime,
            CreatorId = category.CreatorId,
            LastModificationTime = category.LastModificationTime,
            LastModifierId = category.LastModifierId
        };
    }
}
