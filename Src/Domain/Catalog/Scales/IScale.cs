using System.Collections.Immutable;
using TreniniDotNet.Common.Entities;

namespace TreniniDotNet.Domain.Catalog.Scales
{
    public interface IScale : IModifiableEntity, IScaleInfo
    {
        ScaleGauge Gauge { get; }

        string? Description { get; }

        int? Weight { get; }

        IImmutableSet<ScaleStandard> Standards { get; }

        IScaleInfo ToScaleInfo();
    }
}
