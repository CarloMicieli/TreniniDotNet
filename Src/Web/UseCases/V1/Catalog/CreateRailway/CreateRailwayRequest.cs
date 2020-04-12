using MediatR;
using System;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.CreateRailway
{
    public sealed class CreateRailwayRequest : IRequest
    {
        public string Name { get; set; } = null!;
        public string? CompanyName { get; set; }
        public string? Country { get; set; }
        public PeriodOfActivityRequest? PeriodOfActivity { set; get; }
        public TotalRailwayLengthRequest? TotalLength { get; set; }
        public RailwayGaugeRequest? Gauge { get; set; }
        public string? WebsiteUrl { get; set; }
        public string? Headquarters { get; set; }
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
