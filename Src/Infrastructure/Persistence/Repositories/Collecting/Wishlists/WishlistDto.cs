using System;

namespace TreniniDotNet.Infrastructure.Persistence.Repositories.Collecting.Wishlists
{
#pragma warning disable CA1707 // Identifiers should not contain underscores
#pragma warning disable CA1812 // Avoid uninstantiated internal classes
#pragma warning disable IDE1006 // Naming Styles
    internal sealed class WishlistDto
    {
        public Guid wishlist_id { get; set; }
        public string owner { get; set; } = null!;
        public string slug { get; set; } = null!;
        public string? wishlist_name { get; set; }
        public string visibility { get; set; } = null!;
        public decimal? budget_amount { set; get; }
        public string? budget_currency { set; get; }
        public Guid? wishlist_item_id { set; get; }
        public Guid? catalog_item_id { set; get; }
        public string? catalog_item_slug { get; set; }
        public string? brand_name { get; set; }
        public string? item_number { get; set; }
        public string? description { get; set; }
        public string? category { get; set; }
        public string? priority { set; get; }
        public DateTime? added_date { set; get; }
        public DateTime? removed_date { set; get; }
        public decimal? price_amount { set; get; }
        public string? price_currency { set; get; }
        public string? notes { set; get; }
        public DateTime created { get; set; }
        public DateTime? last_modified { get; set; }
        public int version { get; set; }
    }
#pragma warning restore CA1707 // Identifiers should not contain underscores
#pragma warning restore CA1812 // Avoid uninstantiated internal classes
#pragma warning restore IDE1006 // Naming Styles
}
