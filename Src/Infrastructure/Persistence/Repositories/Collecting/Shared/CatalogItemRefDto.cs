using System;

namespace TreniniDotNet.Infrastructure.Persistence.Repositories.Collecting.Shared
{
#pragma warning disable CA1707 // Identifiers should not contain underscores
#pragma warning disable CA1812 // Avoid uninstantiated internal classes
#pragma warning disable IDE1006 // Naming Styles
    internal class CatalogItemRefDto
    {
        public Guid catalog_item_id { set; get; }
        public string brand_name { set; get; } = null!;
        public string item_number { set; get; } = null!;
        public string slug { set; get; } = null!;
        public string description { set; get; } = null!;
        public string category { set; get; } = null!;
#pragma warning restore CA1707 // Identifiers should not contain underscores
#pragma warning restore CA1812 // Avoid uninstantiated internal classes
#pragma warning restore IDE1006 // Naming Styles
    }
}