namespace TreniniDotNet.IntegrationTests.Catalog.V1.Requests
{
    public class ScaleGaugeRequest
    {
        public decimal? Millimeters { set; get; }
        public decimal? Inches { set; get; }
        public string TrackGauge { set; get; }
    }
}