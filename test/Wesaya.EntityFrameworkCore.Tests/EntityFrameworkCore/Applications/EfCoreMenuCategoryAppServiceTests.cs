using System;
using System.Threading.Tasks;
using Shouldly;
using Wesaya.Menu.Categories;
using Wesaya.Menu.Shared;
using Xunit;

namespace Wesaya.EntityFrameworkCore.Applications;

[Collection(WesayaTestConsts.CollectionDefinitionName)]
public class EfCoreMenuCategoryAppServiceTests
    : WesayaApplicationTestBase<WesayaEntityFrameworkCoreTestModule>
{
    private readonly IMenuCategoryAppService _categoryAppService;

    public EfCoreMenuCategoryAppServiceTests()
    {
        _categoryAppService = GetRequiredService<IMenuCategoryAppService>();
    }

    [Fact]
    public async Task Should_Create_And_Retrieve_Menu_Category()
    {
        var created = await _categoryAppService.CreateAsync(CreateInput());

        created.Id.ShouldNotBe(Guid.Empty);
        created.Name.ShouldBe("Test category");
        created.DisplayOrder.ShouldBe(10);
        created.IsActive.ShouldBeTrue();

        var retrieved = await _categoryAppService.GetAsync(created.Id);

        retrieved.Id.ShouldBe(created.Id);
        retrieved.Name.ShouldBe("Test category");
        retrieved.DisplayOrder.ShouldBe(10);
        retrieved.IsActive.ShouldBeTrue();
    }

    private static CreateUpdateMenuCategoryDto CreateInput()
    {
        return new CreateUpdateMenuCategoryDto
        {
            Name = new StrongLocalizedStringInputDto
            {
                English = "Test category",
                Arabic = "Test category Ar"
            },
            DisplayOrder = 10,
            IsActive = true
        };
    }
}
