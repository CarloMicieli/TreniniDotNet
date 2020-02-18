using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Domain.Catalog.Scales
{
    public interface IScalesFactory
    {
        IScale NewScale(string name, Ratio ratio, Gauge gauge, TrackGauge trackGauge, string? notes);

        Scale NewScale(string name, decimal ratio, decimal gauge, TrackGauge trackGauge);
    }
}
