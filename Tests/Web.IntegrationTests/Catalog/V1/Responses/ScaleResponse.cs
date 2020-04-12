using System;
using System.Collections.Generic;

namespace TreniniDotNet.IntegrationTests.Catalog.V1.Responses
{
    internal class ScaleResponse
    {
        public Guid Id { set; get; }
        public SelfLinks _Links { set; get; }
        public string Slug { set; get; }
        public string Name { set; get; }
        public decimal? Ratio { set; get; }
        public ScaleGaugeResponse Gauge { set; get; }
        public List<string> Standards { set; get; }
        public string Notes { set; get; }
    }

    internal class ScaleGaugeResponse
    {
        public decimal? Millimeters { set; get; }
        public decimal? Inches { set; get; }
        public string TrackGauge { set; get; }
    }
}