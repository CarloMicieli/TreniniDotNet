using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Domain.Catalog.Scales
{
    public class ScalesFactory : IScalesFactory
    {
        public IScale NewScale(string name, Ratio ratio, Gauge gauge, TrackGauge trackGauge, string? notes)
        {
            return new Scale(
                ScaleId.NewId(),
                Slug.Of(name),
                name,
                ratio,
                gauge,
                trackGauge,
                notes);
        }

        public IScale NewScale(string name, decimal? ratio, decimal? gauge, TrackGauge trackGauge, string? notes)
        {
            return new Scale(
                ScaleId.NewId(),
                Slug.Of(name),
                name,
                Ratio.Of(ratio ?? 0M),
                Gauge.OfMillimiters(gauge ?? 0M),
                trackGauge,
                notes);
        }
    }
}