using System;

namespace TreniniDotNet.Web.UseCases.V1.Catalog
{
    public sealed class ScaleGaugeRequest
    {
        public string? TrackGauge { set; get; }
        public decimal? Inches { set; get; }
        public decimal? Millimeters { set; get; }
    }

    public sealed class PeriodOfActivityRequest
    {
        public string? Status { get; set; }
        public DateTime? OperatingUntil { get; set; }
        public DateTime? OperatingSince { get; set; }
    }

    public sealed class TotalRailwayLengthRequest
    {
        public string? TrackGauge { get; set; }
        public decimal? Millimeters { get; set; }
        public decimal? Inches { get; set; }
    }

    public sealed class RailwayGaugeRequest
    {
        public decimal? Kilometers { get; set; }
        public decimal? Miles { get; set; }
    }
}
