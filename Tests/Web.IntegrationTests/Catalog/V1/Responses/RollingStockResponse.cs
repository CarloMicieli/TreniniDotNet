using System;

namespace TreniniDotNet.IntegrationTests.Catalog.V1.Responses
{
    public class RollingStockResponse
    {
        public Guid Id { set; get; }

        public RailwayInfoResponse Railway { set; get; }

        public string Category { set; get; }

        public string Era { set; get; }

        public LengthOverBufferResponse LengthOverBuffer { set; get; }

        public string ClassName { set; get; }

        public string RoadNumber { set; get; }

        public string TypeName { set; get; }

        public string DccInterface { set; get; }

        public string Control { set; get; }
    }

    public sealed class LengthOverBufferResponse
    {
        public decimal? Millimeters { set; get; }

        public decimal? Inches { set; get; }
    }
}
