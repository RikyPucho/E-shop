using Eshop.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Eshop.Permissions;

public class EshopPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(EshopPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(EshopPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<EshopResource>(name);
    }
}
