using System.Collections.Generic;

namespace DataSeeding.DataLoader.Records.Catalog.CatalogItems
{
    public sealed class CatalogItem
    {
        public string Brand { get; set; }
        public string ItemNumber { get; set; }
        public string Scale { get; set; }
        public string PowerMethod { get; set; }
        public string Description { get; set; }
        public string PrototypeDescription { get; set; }
        public string ModelDescription { get; set; }
        public string DeliveryDate { get; set; }
        public bool? Available { get; set; }
        public int? Count { get; set; }
        public List<RollingStock> RollingStocks { get; set; }
    }
}
