using System;
using TreniniDotNet.Infrastructure.Persistence.Catalog.Railways;

namespace TreniniDotNet.Infrastracture.Persistence.Catalog.CatalogItems
{
    public class RollingStock
    {
        public Guid RollingStockId { set; get; }

        public string Era { set; get; } = null!;
        
        public decimal? Length { set; get; }
        
        public Railway Railway { set; get; } = null!;
        
        public string? ClassName { set; get; }
        
        public string? RoadNumber { set; get; }

        public string? Livery { set; get; }

        public string? Series { set; get; }

        public string? DepotName { set; get; }

        public string Category { set; get; } = null!;
    }
}
