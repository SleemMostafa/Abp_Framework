using Wesaya.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Wesaya.Permissions;

public sealed class WesayaPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var wesayaGroup = context.AddGroup(WesayaPermissions.GroupName, L("Permission:Wesaya"));

        var menuCategories = wesayaGroup.AddPermission(
            WesayaPermissions.MenuCategories.Default,
            L("Permission:MenuCategories"));

        menuCategories.AddChild(
            WesayaPermissions.MenuCategories.Update,
            L("Permission:MenuCategories.Update"));

        menuCategories.AddChild(
            WesayaPermissions.MenuCategories.Delete,
            L("Permission:MenuCategories.Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<WesayaResource>(name);
    }
}
