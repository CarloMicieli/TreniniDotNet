using System;
using LanguageExt;
using static LanguageExt.Prelude;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using NodaTime;

namespace TreniniDotNet.Domain.Catalog.Scales
{
    public class ScalesFactory : IScalesFactory
    {
        private readonly IClock _clock;

        public ScalesFactory(IClock clock)
        {
            _clock = clock;
        }

        [Obsolete]
        public IScale NewScale(Guid id, string name, string slug, decimal ratio, decimal gauge, string? trackGauge, string? notes)
        {
            return new Scale(
                new ScaleId(id),
                Slug.Of(slug),
                name,
                Ratio.Of(ratio),
                Gauge.OfMillimiters(gauge),
                trackGauge.ToTrackGauge(),
                notes);
        }

        [Obsolete]
        public IScale NewScale(ScaleId id, string name, Slug slug, Ratio ratio, Gauge gauge, TrackGauge trackGauge, string? notes)
        {
            return new Scale(
                id,
                slug,
                name,
                ratio,
                gauge,
                trackGauge,
                notes);
        }

        public Validation<Error, IScale> NewScaleV(
            Guid id,
            string name,
            decimal ratio,
            decimal gauge, string? trackGauge,
            string? notes)
        {
            var nameV = ToScaleName(name);
            var ratioV = ToRatio(ratio);
            var gaugeV = ToGauge(gauge);
            var trackGaugeV = ToTrackGauge(trackGauge);

            return (nameV, ratioV, gaugeV, trackGaugeV).Apply((_name, _ratio, _gauge, _trackGauge) =>
            {
                IScale scale = new Scale(
                    new ScaleId(id),
                    _name,
                    Slug.Of(_name),
                    _ratio,
                    _gauge,
                    _trackGauge,
                    notes,
                    _clock.GetCurrentInstant(),
                    1);
                return scale;
            });
        }

        public static Validation<Error, string> ToScaleName(string? str) =>
            string.IsNullOrWhiteSpace(str) == false ? Success<Error, string>(str!) : Fail<Error, string>(Error.New("invalid scale: name cannot be empty"));

        private static Validation<Error, Ratio> ToRatio(decimal ratio) =>
            ratio > 0M ? Success<Error, Ratio>(Ratio.Of(ratio)) : Fail<Error, Ratio>(Error.New("ratio value must be positive"));

        private static Validation<Error, Gauge> ToGauge(decimal gauge) =>
            gauge > 0M ? Success<Error, Gauge>(Gauge.OfMillimiters(gauge)) : Fail<Error, Gauge>(Error.New("gauge value must be positive"));

        private static Validation<Error, TrackGauge> ToTrackGauge(string? str) =>
            TrackGauges.TryParse(str, out var trackGauge) ? Success<Error, TrackGauge>(trackGauge) : Fail<Error, TrackGauge>(Error.New($"'{str}' is not a valid track gauge"));
    }
}