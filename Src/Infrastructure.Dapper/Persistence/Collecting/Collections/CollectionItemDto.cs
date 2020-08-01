using System;

namespace TreniniDotNet.Infrastructure.Persistence.Collecting.Collections
{
#pragma warning disable CA1707 // Identifiers should not contain underscores
#pragma warning disable CA1812 // Avoid uninstantiated internal classes
#pragma warning disable IDE1006 // Naming Styles
    internal class CollectionItemDto
    {
        public Guid item_id { get; set; }
        public Guid collection_id { get; set; }
        public Guid catalog_item_id { get; set; }
        public string catalog_item_slug { get; set; } = null!;
        public string condition { get; set; } = null!;
        public decimal price { get; set; }
        public string currency { get; set; } = null!;
        public Guid? shop_id { get; set; }
        public string? shop_name { get; set; }
        public string? shop_slug { get; set; }
        public DateTime added_date { get; set; }
        public DateTime? removed_date { get; set; }
        public string? notes { get; set; }
    }
#pragma warning restore CA1707 // Identifiers should not contain underscores
#pragma warning restore CA1812 // Avoid uninstantiated internal classes
#pragma warning restore IDE1006 // Naming Styles
}
