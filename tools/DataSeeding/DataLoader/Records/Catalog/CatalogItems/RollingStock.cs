using System;

namespace DataSeeding.DataLoader.Records.Catalog.CatalogItems
{
    public sealed class RollingStock
    {
        public string Railway { get; set; }
        public string Category { get; set; }
        public string Epoch { get; set; }
        public Length Length { get; set; }
        public string ClassName { get; set; }
        public string RoadNumber { get; set; }
        public string TypeName { get; set; }
        public string Depot { get; set; }
        public string Series { get; set; }
        public string Livery { get; set; }
        public string Couplers { get; set; }
        public int? MinRadius { get; set; }
        public string PassengerCarType { get; set; }
        public string ServiceLevel { get; set; }
        public string DccInterface { get; set; }
        public string Control { get; set; }
    }
}
