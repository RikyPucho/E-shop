var prodottiPermission = EshopGroup.AddPermission(EshopPermissions.Prodotti.Default, L("Permission:Prodotti"));
    prodottiPermission.AddChild(EshopPermissions.Prodotti.Create, L("Permission:Prodotti.Create"));
    prodottiPermission.AddChild(EshopPermissions.Prodotti.Edit, L("Permission:Prodotti.Edit"));
    prodottiPermission.AddChild(EshopPermissions.Prodotti.Delete, L("Permission:Prodotti.Delete"));
