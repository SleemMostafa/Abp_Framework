namespace Wesaya.Permissions;

public static class WesayaPermissions
{
    public const string GroupName = "Wesaya";

    public static class MenuCategories
    {
        public const string Default = GroupName + ".MenuCategories";
        public const string Update = Default + ".Update";
        public const string Delete = Default + ".Delete";
    }
}
