using System;
using TreniniDotNet.Common.Interfaces;

namespace TreniniDotNet.Application.Boundaries.Catalog.CreateRailway
{
    public sealed class CreateRailwayInput : IUseCaseInput
    {
        public string? Name { get; }

        public string? CompanyName { get; }

        public string? Country { get; }

        public PeriodOfActivityInput PeriodOfActivity { get; }

        public TotalRailwayLengthInput TotalLength { get; }

        public RailwayGaugeInput Gauge { get; }

        public string? WebsiteUrl { get; }

        public string? Headquarters { get; }

        public CreateRailwayInput(
            string? name, string? companyName,
            string? country,
            PeriodOfActivityInput? periodOfActivity,
            TotalRailwayLengthInput? totalLength,
            RailwayGaugeInput? gauge,
            string? websiteUrl, string? headquarters)
        {
            Name = name;
            CompanyName = companyName;
            Country = country;
            PeriodOfActivity = periodOfActivity ?? PeriodOfActivityInput.Default();
            TotalLength = totalLength ?? TotalRailwayLengthInput.Default();
            Gauge = gauge ?? RailwayGaugeInput.Default();
            Headquarters = headquarters;
            WebsiteUrl = websiteUrl;
        }
    }

    public sealed class PeriodOfActivityInput
    {
        public PeriodOfActivityInput(
            string? status,
            DateTime? operatingSince,
            DateTime? operatingUntil)
        {
            Status = status;
            OperatingUntil = operatingUntil;
            OperatingSince = operatingSince;
        }

        public string? Status { get; }
        public DateTime? OperatingUntil { get; }
        public DateTime? OperatingSince { get; }

        public void Deconstruct(out string? status, out DateTime? since, out DateTime? until)
        {
            status = Status;
            since = OperatingSince;
            until = OperatingUntil;
        }

        public static PeriodOfActivityInput Default() =>
            new PeriodOfActivityInput(null, null, null);
    }

    public sealed class TotalRailwayLengthInput
    {
        public TotalRailwayLengthInput(decimal? kilometers, decimal? miles)
        {
            Kilometers = kilometers;
            Miles = miles;
        }

        public decimal? Kilometers { get; }
        public decimal? Miles { get; }

        public void Deconstruct(out decimal? km, out decimal? mi)
        {
            km = Kilometers;
            mi = Miles;
        }

        public static TotalRailwayLengthInput Default() =>
            new TotalRailwayLengthInput(null, null);
    }

    public sealed class RailwayGaugeInput
    {
        public RailwayGaugeInput(string? trackGauge, decimal? millimeters, decimal? inches)
        {
            TrackGauge = trackGauge;
            Millimeters = millimeters;
            Inches = inches;
        }

        public string? TrackGauge { get; }
        public decimal? Millimeters { get; }
        public decimal? Inches { get; }

        public void Deconstruct(out string? trackGauge, out decimal? mm, out decimal? inches)
        {
            trackGauge = TrackGauge;
            mm = Millimeters;
            inches = Inches;
        }

        public static RailwayGaugeInput Default() =>
            new RailwayGaugeInput(null, null, null);
    }
}
