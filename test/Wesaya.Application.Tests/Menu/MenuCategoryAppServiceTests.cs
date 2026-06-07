using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using NSubstitute;
using Shouldly;
using Volo.Abp.Authorization;
using Wesaya.Menu.Categories;
using Wesaya.Menu.Categories.Commands;
using Wesaya.Menu.Categories.Queries;
using Wesaya.Menu.Shared;
using Wesaya.Permissions;
using Xunit;

namespace Wesaya.Menu;

public class MenuCategoryAppServiceTests
{
    private readonly ISender _sender;
    private readonly MenuCategoryAppService _categoryAppService;

    public MenuCategoryAppServiceTests()
    {
        _sender = Substitute.For<ISender>();
        _categoryAppService = new MenuCategoryAppService(_sender);
    }

    [Fact]
    public async Task CreateAsync_Should_Send_Create_Command()
    {
        var input = CreateInput("Starters", "Starters Ar");
        var expected = new MenuCategoryDto
        {
            Id = Guid.NewGuid(),
            Name = "Starters",
            DisplayOrder = 10,
            IsActive = true
        };

        _sender
            .Send(
                Arg.Is<CreateMenuCategoryCommand>(command => command.Input == input),
                Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(expected));

        var result = await _categoryAppService.CreateAsync(input);

        result.ShouldBeSameAs(expected);
        await _sender
            .Received(1)
            .Send(
                Arg.Is<CreateMenuCategoryCommand>(command => command.Input == input),
                Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task GetAsync_Should_Send_Get_Query()
    {
        var categoryId = Guid.NewGuid();
        var expected = new MenuCategoryDto
        {
            Id = categoryId,
            Name = "Starters",
            DisplayOrder = 10,
            IsActive = true
        };

        _sender
            .Send(
                Arg.Is<GetMenuCategoryQuery>(query => query.Id == categoryId),
                Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(expected));

        var result = await _categoryAppService.GetAsync(categoryId);

        result.ShouldBeSameAs(expected);
        await _sender
            .Received(1)
            .Send(
                Arg.Is<GetMenuCategoryQuery>(query => query.Id == categoryId),
                Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task UpdateAsync_Should_Send_Update_Command()
    {
        var categoryId = Guid.NewGuid();
        var input = CreateInput("Small Plates", "Small Plates Ar");
        var expected = new MenuCategoryDto
        {
            Id = categoryId,
            Name = "Small Plates",
            DisplayOrder = 10,
            IsActive = true
        };

        _sender
            .Send(
                Arg.Is<UpdateMenuCategoryCommand>(command =>
                    command.Id == categoryId && command.Input == input),
                Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(expected));

        var result = await _categoryAppService.UpdateAsync(categoryId, input);

        result.ShouldBeSameAs(expected);
        await _sender
            .Received(1)
            .Send(
                Arg.Is<UpdateMenuCategoryCommand>(command =>
                    command.Id == categoryId && command.Input == input),
                Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task DeleteAsync_Should_Send_Delete_Command()
    {
        var categoryId = Guid.NewGuid();

        await _categoryAppService.DeleteAsync(categoryId);

        await _sender
            .Received(1)
            .Send(
                Arg.Is<DeleteMenuCategoryCommand>(command => command.Id == categoryId),
                Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task UpdateAsync_Should_Fail_Authorization_When_Update_Permission_Is_Not_Granted()
    {
        var policyName = GetAuthorizeAttribute(nameof(MenuCategoryAppService.UpdateAsync)).Policy;

        policyName.ShouldBe(WesayaPermissions.MenuCategories.Update);
        await Should.ThrowAsync<AbpAuthorizationException>(() =>
            new DenyAuthorizationService().CheckAsync(policyName!));
    }

    [Fact]
    public async Task DeleteAsync_Should_Fail_Authorization_When_Delete_Permission_Is_Not_Granted()
    {
        var policyName = GetAuthorizeAttribute(nameof(MenuCategoryAppService.DeleteAsync)).Policy;

        policyName.ShouldBe(WesayaPermissions.MenuCategories.Delete);
        await Should.ThrowAsync<AbpAuthorizationException>(() =>
            new DenyAuthorizationService().CheckAsync(policyName!));
    }

    private static CreateUpdateMenuCategoryDto CreateInput(string englishName, string arabicName)
    {
        return new CreateUpdateMenuCategoryDto
        {
            Name = new StrongLocalizedStringInputDto
            {
                English = englishName,
                Arabic = arabicName
            },
            DisplayOrder = 10,
            IsActive = true
        };
    }

    private static AuthorizeAttribute GetAuthorizeAttribute(string methodName)
    {
        var method = typeof(MenuCategoryAppService)
            .GetMethods(BindingFlags.Instance | BindingFlags.Public)
            .Single(method => method.Name == methodName);

        return method.GetCustomAttribute<AuthorizeAttribute>()
               ?? throw new InvalidOperationException($"{methodName} is not protected by authorization.");
    }

    private sealed class DenyAuthorizationService : IAbpAuthorizationService
    {
        public IServiceProvider ServiceProvider { get; set; } = default!;

        public ClaimsPrincipal CurrentPrincipal { get; set; } = new(new ClaimsIdentity());

        public Task<AuthorizationResult> AuthorizeAsync(
            ClaimsPrincipal user,
            object? resource,
            IEnumerable<IAuthorizationRequirement> requirements)
        {
            return Task.FromResult(AuthorizationResult.Failed());
        }

        public Task<AuthorizationResult> AuthorizeAsync(
            ClaimsPrincipal user,
            object? resource,
            string policyName)
        {
            return Task.FromResult(AuthorizationResult.Failed());
        }
    }
}
