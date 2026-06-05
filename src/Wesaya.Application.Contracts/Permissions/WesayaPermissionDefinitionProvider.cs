using Wesaya.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Wesaya.Permissions;

public class WesayaPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(WesayaPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(WesayaPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<WesayaResource>(name);
    }
}
