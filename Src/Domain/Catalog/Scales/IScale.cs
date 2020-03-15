using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Common;

namespace TreniniDotNet.Domain.Catalog.Scales
{
    public interface IScale : IScaleInfo
    {
        ScaleId ScaleId { get; }

        Gauge Gauge { get; }

        TrackGauge TrackGauge { get; }

        string? Notes { get; }
    }
}
