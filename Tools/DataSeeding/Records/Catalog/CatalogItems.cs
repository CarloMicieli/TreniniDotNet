using System;
using System.Collections.Generic;

namespace DataSeeding.Records.Catalog
{
    public sealed class CatalogItems : DataSet<CatalogItem> { }

    public sealed class CatalogItem
    {
        public Guid ItemId { get; set; }
        public string Brand { get; set; }
        public string ItemNumber { get; set; }
        public string Slug { get; set; }
        public string Scale { get; set; }
        public string PowerMethod { get; set; }
        public string Description { get; set; }
        public string DeliveryDate { get; set; }
        public bool? Available { get; set; }
        public int? Count { get; set; }
        public List<RollingStock> RollingStocks { get; set; }
        public DateTime? Created { get; set; }
        public int Version { get; set; }
    }

    public sealed class RollingStock
    {
        public Guid Id { get; set; }
        public string Railway { get; set; }
        public string Category { get; set; }
        public string Era { get; set; }
        public decimal? Length { get; set; }
        public string ClassName { get; set; }
        public string RoadNumber { get; set; }
        public string TypeName { get; set; }
        public string DccInterface { get; set; }
        public string Control { get; set; }
    }
}
