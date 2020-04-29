namespace TreniniDotNet.Web.Catalog.V1.Railways.Common.Requests
{
    public sealed class TotalRailwayLengthRequest
    {
        public string? TrackGauge { get; set; }
        public decimal? Millimeters { get; set; }
        public decimal? Inches { get; set; }
    }
}