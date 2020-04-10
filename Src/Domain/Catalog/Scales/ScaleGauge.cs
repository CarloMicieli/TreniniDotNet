using System;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Common.Lengths;

namespace TreniniDotNet.Domain.Catalog.Scales
{
    public sealed class ScaleGauge : IEquatable<ScaleGauge>
    {
        private static TwoGauges gauges = new TwoGauges();

        private ScaleGauge(Gauge millimetres, Gauge inches, TrackGauge trackGauge)
        {
            InMillimeters = millimetres;
            InInches = inches;
            TrackGauge = trackGauge;
        }

        public static ScaleGauge Of(decimal value, MeasureUnit mu, TrackGauge trackGauge = TrackGauge.Standard)
        {
            var (mm, ins) = gauges.Create(value, mu);
            return new ScaleGauge(millimetres: mm, inches: ins, trackGauge);
        }

        public static ScaleGauge Of(decimal? millimeters, decimal? inches, string trackGauge)
        {
            var (mm, ins) = gauges.Create(millimeters, inches);
            return new ScaleGauge(millimetres: mm, inches: ins, trackGauge.ToTrackGauge());
        }

        public Gauge InMillimeters { get; }
        public Gauge InInches { get; }

        public TrackGauge TrackGauge { get; }

        #region [ Equality ]

        public override bool Equals(object obj)
        {
            if (obj is ScaleGauge that)
            {
                return AreEquals(this, that);
            }

            return false;
        }

        public bool Equals(ScaleGauge other)
        {
            return AreEquals(this, other);
        }

        private static bool AreEquals(ScaleGauge left, ScaleGauge right) =>
            left.InInches.Equals(right.InInches) &&
            left.InMillimeters.Equals(right.InMillimeters) &&
            left.TrackGauge.Equals(right.TrackGauge);

        #endregion  

        public override int GetHashCode() =>
            HashCode.Combine(InInches, InMillimeters, TrackGauge);

        public override string ToString() => $"Gauge({TrackGauge}, {InMillimeters}, {InInches})";
    }
}