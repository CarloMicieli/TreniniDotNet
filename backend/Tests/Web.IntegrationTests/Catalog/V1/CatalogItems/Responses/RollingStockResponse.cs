using System;
using TreniniDotNet.IntegrationTests.Catalog.V1.Railways.Responses;

namespace TreniniDotNet.IntegrationTests.Catalog.V1.CatalogItems.Responses
{
    public class RollingStockResponse
    {
        public Guid Id { set; get; }

        public RailwayInfoResponse Railway { set; get; }

        public string Category { set; get; }

        public string Epoch { set; get; }

        public LengthOverBufferResponse LengthOverBuffer { set; get; }

        public MinRadiusResponse MinRadius { set; get; }

        public string ClassName { set; get; }

        public string RoadNumber { set; get; }

        public string TypeName { set; get; }

        public string Series { set; get; }

        public string Couplers { set; get; }

        public string Livery { set; get; }

        public string Depot { set; get; }

        public string DccInterface { set; get; }

        public string Control { set; get; }

        public string ServiceLevel { set; get; }
    }

    public sealed class LengthOverBufferResponse
    {
        public decimal? Millimeters { set; get; }

        public decimal? Inches { set; get; }
    }

    public sealed class MinRadiusResponse
    {
        public decimal? Millimeters { set; get; }
    }
}
