using TreniniDotNet.Domain.Catalog.Railways;

namespace TreniniDotNet.Application.Boundaries.Common
{
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

        public RailwayGauge? ToRailwayGauge()
        {
            var (trackGauge, mm, inches) = this;
            return RailwayGauge.TryCreate(trackGauge, inches, mm, out var rg) ? rg : null;
        }

        public static RailwayGaugeInput Default() =>
            new RailwayGaugeInput(null, null, null);
    }
}
