using System;

namespace TreniniDotNet.Infrastructure.Persistence.Repositories.Collecting.Wishlists
{
#pragma warning disable CA1707 // Identifiers should not contain underscores
#pragma warning disable CA1812 // Avoid uninstantiated internal classes
#pragma warning disable IDE1006 // Naming Styles
    internal class WishlistItemDto
    {
        public Guid item_id { get; set; }
        public Guid wishlist_id { get; set; }
        public Guid catalog_item_id { get; set; }
        public string catalog_item_slug { get; set; } = null!;
        public string priority { get; set; } = null!;
        public DateTime added_date { get; set; }
        public DateTime? removed_date { get; set; }
        public decimal? price { get; set; }
        public string? currency { get; set; }
        public string? notes { get; set; }
    }
#pragma warning restore CA1707 // Identifiers should not contain underscores
#pragma warning restore CA1812 // Avoid uninstantiated internal classes
#pragma warning restore IDE1006 // Naming Styles
}
