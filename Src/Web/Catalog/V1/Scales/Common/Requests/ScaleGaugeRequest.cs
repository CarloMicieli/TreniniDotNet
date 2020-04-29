namespace TreniniDotNet.Web.Catalog.V1.Scales.Common.Requests
{
    public sealed class ScaleGaugeRequest
    {
        public string? TrackGauge { set; get; }
        public decimal? Inches { set; get; }
        public decimal? Millimeters { set; get; }
    }
}