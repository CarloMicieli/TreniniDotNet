using LanguageExt;
using static LanguageExt.Prelude;
using System;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Common.Extensions;

namespace TreniniDotNet.Domain.Catalog.Scales
{
    public sealed class ScaleGauge : IEquatable<ScaleGauge>
    {
        private ScaleGauge(Gauge millimetres, Gauge inches, TrackGauge trackGauge)
        {
            InMillimeters = millimetres;
            InInches = inches;
            TrackGauge = trackGauge;
        }

        public static ScaleGauge Of(decimal value, MeasureUnit mu, TrackGauge trackGauge = TrackGauge.Standard)
        {
            return new ScaleGauge(
                millimetres: Gauge.OfMillimiters(MeasureUnit.Millimeters.GetValueOrConvert(() => (value, mu))),
                inches: Gauge.OfInches(MeasureUnit.Inches.GetValueOrConvert(() => (value, mu))),
                trackGauge);
        }

        public static ScaleGauge Of(decimal millimeters, decimal inches, string trackGauge) =>
            new ScaleGauge(Gauge.OfMillimiters(millimeters), Gauge.OfInches(inches), trackGauge.ToTrackGauge());

        public static Option<ScaleGauge> TryConvert(decimal millimeters, decimal inches, string trackGauge)
        {
            var result = (
                from _mm in Gauge.TryConvert(millimeters, MeasureUnit.Millimeters)
                from _in in Gauge.TryConvert(inches, MeasureUnit.Inches)
                select new ScaleGauge(_mm, _in, trackGauge.ToTrackGauge()));

            return result;
        }

        public static Validation<Error, ScaleGauge> TryCreate(decimal? millimeters, decimal? inches, string? trackGauge)
        {
            if (millimeters.HasValue && inches.HasValue)
            {
                var toMillimeters = NonNegative(millimeters.Value, "{0} millimeters is not a valid value for gauges");
                var toInches = NonNegative(inches.Value, "{0} inches is not a valid value for gauges");
                var toTrackGauge = ValidTrackGauge(trackGauge);

                return (toMillimeters, toInches, toTrackGauge).Apply((_mm, _in, _tg) =>
                {
                    return new ScaleGauge(
                        Gauge.OfMillimiters(_mm),
                        Gauge.OfInches(_in),
                        _tg);
                });
            }
            else if (millimeters.HasValue)
            {
                var toMillimeters = NonNegative(millimeters.Value, "{0} millimeters is not a valid value for gauges");
                var toTrackGauge = ValidTrackGauge(trackGauge);

                return (toMillimeters, toTrackGauge).Apply((_mm, _tg) =>
                {
                    return ScaleGauge.Of(_mm, MeasureUnit.Millimeters, _tg);
                });
            }
            else if (inches.HasValue)
            {
                var toInches = NonNegative(inches.Value, "{0} inches is not a valid value for gauges");
                var toTrackGauge = ValidTrackGauge(trackGauge);

                return (toInches, toTrackGauge).Apply((_in, _tg) =>
                {
                    return ScaleGauge.Of(_in, MeasureUnit.Millimeters, _tg);
                });
            }

            return Fail<Error, ScaleGauge>("unable to create a scale gauge");
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

        private static Validation<Error, decimal> NonNegative(decimal value, string because) =>
            value.IsPositive() ? Success<Error, decimal>(value) : Fail<Error, decimal>(string.Format(because, value));

        private static Validation<Error, TrackGauge> ValidTrackGauge(string? str) =>
            Enum.TryParse<TrackGauge>(str, out var result) ?
                Success<Error, TrackGauge>(result) :
                Fail<Error, TrackGauge>(Error.New($"'{str}' is not a valid track gauge"));
    }
}