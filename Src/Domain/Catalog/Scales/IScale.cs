using System.Collections.Immutable;
using NodaTime;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Domain.Catalog.Scales
{
    public interface IScale : IScaleInfo
    {
        ScaleGauge Gauge { get; }

        string? Description { get; }

        int? Weight { get; }

        IImmutableSet<ScaleStandard> Standards { get; }

        Instant CreatedDate { get; }

        Instant? ModifiedDate { get; }

        int Version { get; }

        IScaleInfo ToScaleInfo();

        IScale With(
            Ratio? ratio = null,
            ScaleGauge? gauge = null,
            string? description = null,
            IImmutableSet<ScaleStandard>? standards = null,
            int? weight = null) => new Scale(Id,
            Name,
            Slug,
            ratio ?? Ratio,
            gauge ?? Gauge,
            description ?? Description,
            standards ?? Standards,
            weight ?? Weight,
            CreatedDate,
            ModifiedDate,
            Version);
    }
}
