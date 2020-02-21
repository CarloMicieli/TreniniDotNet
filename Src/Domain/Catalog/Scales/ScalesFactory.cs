using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Domain.Catalog.Scales
{
    public class ScalesFactory : IScalesFactory
    {
        public IScale NewScale(string name, Ratio ratio, Gauge gauge, TrackGauge trackGauge, string? notes)
        {
            throw new System.NotImplementedException();
        }

        public IScale NewScale(string name, decimal? ratio, decimal? gauge, TrackGauge trackGauge)
        {
            //TODO
            return new Scale(name, Ratio.Of((float) (ratio ?? 0M)), Gauge.OfMillimiters(gauge ?? 0), trackGauge, null);
        }
    }
}