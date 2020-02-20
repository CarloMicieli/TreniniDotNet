using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Domain.Catalog.Scales
{
    public class ScalesFactory : IScalesFactory
    {
        public IScale NewScale(string name, Ratio ratio, Gauge gauge, TrackGauge trackGauge, string? notes)
        {
            throw new System.NotImplementedException();
        }

        public Scale NewScale(string name, decimal ratio, decimal gauge, TrackGauge trackGauge)
        {
            //TODO
            return new Scale(name, Ratio.Of((float) ratio), Gauge.OfMillimiters(gauge), trackGauge, null);
        }
    }
}