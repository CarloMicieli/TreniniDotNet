﻿using System;

namespace TreniniDotNet.Infrastructure.Persistence.Catalog.CatalogItems
{
#pragma warning disable CA1707 // Identifiers should not contain underscores
#pragma warning disable CA1812 // Avoid uninstantiated internal classes
#pragma warning disable IDE1006 // Naming Styles
    internal class RollingStockDto
    {
        public Guid rolling_stock_id { set; get; } = default;
        public string era { set; get; } = null!;
        public string category { set; get; } = null!;
        public Guid railway_id { get; set; } = default;
        public Guid catalog_item_id { get; set; } = default;
        public decimal? length { get; set; }
        public string? class_name { get; set; }
        public string? road_number { get; set; }
    }
#pragma warning restore CA1707 // Identifiers should not contain underscores
#pragma warning restore CA1812 // Avoid uninstantiated internal classes
#pragma warning restore IDE1006 // Naming Styles
}
