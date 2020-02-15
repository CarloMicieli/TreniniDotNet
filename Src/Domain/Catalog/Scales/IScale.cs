using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Common;

namespace TreniniDotNet.Domain.Catalog.Scales
{
    public interface IScale
    {
        ScaleId ScaleId { get; }

        Slug Slug { get; }

        string Name { get; }

        Ratio Ratio { get; }

        Gauge Gauge { get; }

        TrackGauge TrackGauge { get; }

        string? Notes { get; }
    }
}
