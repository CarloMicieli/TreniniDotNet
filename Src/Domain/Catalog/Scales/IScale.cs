using NodaTime;
using System.Collections.Immutable;

namespace TreniniDotNet.Domain.Catalog.Scales
{
    public interface IScale : IScaleInfo
    {
        ScaleGauge Gauge { get; }

        string? Description { get; }

        int? Weight { get; }

        IImmutableSet<ScaleStandard> Standards { get; }

        Instant? LastModifiedAt { get; }

        int? Version { get; }

        IScaleInfo ToScaleInfo();
    }
}
