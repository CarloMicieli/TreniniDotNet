using TreniniDotNet.Domain.Catalog.Scales;

namespace TreniniDotNet.Application.Catalog.Scales
{
    public sealed class ScaleGaugeInput
    {
        public ScaleGaugeInput(string? trackGauge, decimal? inches, decimal? millimeters)
        {
            TrackGauge = trackGauge;
            Inches = inches;
            Millimeters = millimeters;
        }

        public string? TrackGauge { get; }
        public decimal? Inches { get; }
        public decimal? Millimeters { get; }

        public void Deconstruct(out string? trackGauge, out decimal? inches, out decimal? millimeters)
        {
            trackGauge = TrackGauge;
            inches = Inches;
            millimeters = Millimeters;
        }

        public ScaleGauge ToScaleGauge()
        {
            var (trackType, inches, mm) = this;
            return ScaleGauge.Of(mm, inches, trackType ?? Domain.Catalog.ValueObjects.TrackGauge.Standard.ToString());
        }

        public static ScaleGaugeInput Default() =>
            new ScaleGaugeInput(null, null, null);
    }
}
