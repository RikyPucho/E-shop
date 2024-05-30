namespace Eshop.Permissions;

public static class EshopPermissions
{
    public const string GroupName = "Eshop";

    public static class Prodotti
    {
        public const string Default = GroupName + ".Prodotti";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }
    public static class Carrelli
    {
        public const string Default = GroupName + ".Carrelli";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }
    //Add your own permission names. Example:
    //public const string MyPermission1 = GroupName + ".MyPermission1";
}
