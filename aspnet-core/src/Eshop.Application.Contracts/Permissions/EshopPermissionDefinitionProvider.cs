using Eshop.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Eshop.Permissions;

public class EshopPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var EshopGroup = context.AddGroup(EshopPermissions.GroupName);


        var prodottiPermission = EshopGroup.AddPermission(EshopPermissions.Prodotti.Default, L("Permission:Prodotti"));
        prodottiPermission.AddChild(EshopPermissions.Prodotti.Create, L("Permission:Prodotti.Create"));
        prodottiPermission.AddChild(EshopPermissions.Prodotti.Edit, L("Permission:Prodotti.Edit"));
        prodottiPermission.AddChild(EshopPermissions.Prodotti.Delete, L("Permission:Prodotti.Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<EshopResource>(name);
    }
}
