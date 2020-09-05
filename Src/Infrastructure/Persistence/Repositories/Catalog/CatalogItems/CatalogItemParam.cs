using System;

namespace TreniniDotNet.Infrastructure.Persistence.Repositories.Catalog.CatalogItems
{
#pragma warning disable CA1707 // Identifiers should not contain underscores
#pragma warning disable CA1812 // Avoid uninstantiated internal classes
#pragma warning disable IDE1006 // Naming Styles
    internal class CatalogItemParam
    {
        public Guid catalog_item_id { set; get; }

        public Guid brand_id { get; set; }

        public Guid scale_id { get; set; }

        public string item_number { get; set; } = null!;

        public string slug { get; set; } = null!;

        public string power_method { get; set; } = null!;

        public string? delivery_date { get; set; }

        public bool? available { get; set; }

        public string description { get; set; } = null!;

        public string? model_description { get; set; }

        public string? prototype_description { get; set; }

        public DateTime created { get; set; }

        public DateTime? last_modified { get; set; }

        public int? version { get; set; }
    }
#pragma warning restore CA1707 // Identifiers should not contain underscores
#pragma warning restore CA1812 // Avoid uninstantiated internal classes
#pragma warning restore IDE1006 // Naming Styles    
}