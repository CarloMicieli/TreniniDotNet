using System;

namespace TreniniDotNet.Infrastructure.Persistence.Catalog.CatalogItems
{
    internal class CatalogItemDto
    {
#pragma warning disable CA1707 // Identifiers should not contain underscores
#pragma warning disable IDE1006 // Naming Styles
        public Guid catalog_item_id { set; get; }
        public Guid brand_id { set; get; }
        public string item_number { set; get; } = null!;
        public string slug { set; get; } = null!;
        public string power_method { set; get; } = null!;
        public string? delivery_date { set; get; }
        public string description { set; get; } = null!;
        public string? model_description { set; get; }
        public string? prototype_description { set; get; }
        public DateTime? created_at { set; get; }
        public int? version { set; get; }
#pragma warning restore CA1707 // Identifiers should not contain underscores
#pragma warning restore IDE1006 // Naming Styles
    }
}