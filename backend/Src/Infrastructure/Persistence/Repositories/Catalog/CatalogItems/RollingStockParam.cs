using System;

namespace TreniniDotNet.Infrastructure.Persistence.Repositories.Catalog.CatalogItems
{
#pragma warning disable CA1707 // Identifiers should not contain underscores
#pragma warning disable CA1812 // Avoid uninstantiated internal classes
#pragma warning disable IDE1006 // Naming Styles
    internal class RollingStockParam
    {
        public Guid rolling_stock_id { get; set; }

        public Guid catalog_item_id { get; set; }

        public Guid railway_id { get; set; }

        public string category { get; set; } = null!;

        public string epoch { get; set; } = null!;

        public string? couplers { get; set; }

        public string? livery { get; set; }

        public decimal? length_mm { get; set; }

        public decimal? length_in { get; set; }

        public decimal? min_radius { get; set; }

        public string? type_name { get; set; }

        public string? class_name { get; set; }

        public string? road_number { get; set; }

        public string? series { get; set; }

        public string? depot { get; set; }

        public string? dcc_interface { get; set; }

        public string? control { get; set; }

        public string? passenger_car_type { get; set; }

        public string? service_level { get; set; }
    }
#pragma warning restore CA1707 // Identifiers should not contain underscores
#pragma warning restore CA1812 // Avoid uninstantiated internal classes
#pragma warning restore IDE1006 // Naming Styles    
}