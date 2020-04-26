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

    public sealed class RollingStockRequest
    {
        public string? Era { set; get; }

        public LengthOverBufferRequest? Length { set; get; }

        public string? Railway { set; get; }

        public string? ClassName { set; get; }

        public string? RoadNumber { set; get; }

        public string? TypeName { get; }

        public string? DccInterface { get; }

        public string? Control { get; }

        public string? Category { set; get; }
    }

    public sealed class LengthOverBufferRequest
    {
        public decimal? Millimeters { get; set; }
        public decimal? Inches { get; set; }
    }
}
