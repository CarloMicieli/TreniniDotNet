namespace TreniniDotNet.Infrastructure.Persistence.Repositories.Collecting.Wishlists
{
#pragma warning disable CA1707 // Identifiers should not contain underscores
#pragma warning disable CA1812 // Avoid uninstantiated internal classes
#pragma warning disable IDE1006 // Naming Styles
    internal class WishlistItemWithDataParam : WishlistItemParam
    {
        public string brand_name { set; get; } = null!;
        public string brand_slug { set; get; } = null!;
        public string item_number { set; get; } = null!;
        public string scale_name { set; get; } = null!;
        public string scale_slug { set; get; } = null!;
        public string description { set; get; } = null!;
        public int rolling_stock_count { set; get; }
        public string? category_1 { set; get; }
        public string? category_2 { set; get; }
    }
#pragma warning restore CA1707 // Identifiers should not contain underscores
#pragma warning restore CA1812 // Avoid uninstantiated internal classes
#pragma warning restore IDE1006 // Naming Styles
}
