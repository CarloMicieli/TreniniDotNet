namespace TreniniDotNet.Infrastructure.Persistence.Repositories.Collecting.Collections
{
#pragma warning disable CA1707 // Identifiers should not contain underscores
#pragma warning disable CA1812 // Avoid uninstantiated internal classes
#pragma warning disable IDE1006 // Naming Styles
    internal class CollectionItemWithDataDto : CollectionItemDto
    {
        public string brand_name { get; set; } = null!;
        public string brand_slug { get; set; } = null!;
        public string item_number { get; set; } = null!;
        public string scale_name { get; set; } = null!;
        public string scale_slug { get; set; } = null!;
        public int rolling_stock_count { get; set; }
        public string description { get; set; } = null!;
        public string? category_1 { get; set; }
        public string? category_2 { get; set; }
    }
#pragma warning restore CA1707 // Identifiers should not contain underscores
#pragma warning restore CA1812 // Avoid uninstantiated internal classes
#pragma warning restore IDE1006 // Naming Styles
}
