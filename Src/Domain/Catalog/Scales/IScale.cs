using System.Collections.Immutable;
using NodaTime;
using TreniniDotNet.Common.Entities;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Domain.Catalog.Scales
{
    public interface IScale : IModifiableEntity, IScaleInfo
    {
        ScaleGauge Gauge { get; }

        string? Description { get; }

        int? Weight { get; }

        IImmutableSet<ScaleStandard> Standards { get; }

        IScaleInfo ToScaleInfo();

        IScale With(
            Ratio? ratio = null,
            ScaleGauge? gauge = null,
            string? description = null,
            IImmutableSet<ScaleStandard>? standards = null,
            int? weight = null) => new Scale(ScaleId,
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
