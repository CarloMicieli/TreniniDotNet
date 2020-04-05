using System.Collections.Generic;

namespace TreniniDotNet.IntegrationTests.Catalog.V1.Requests
{
    internal class NewScaleRequest
    {
        public string Name { set; get; }
        public decimal? Ratio { set; get; }
        public ScaleGaugeRequest Gauge { set; get; }
        public string Notes { set; get; }
        public int? Weight { get; }
        public List<string> Standards { get; set; } = new List<string>();
    }
}