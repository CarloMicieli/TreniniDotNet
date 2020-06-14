using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using NodaTime;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.ValueObjects;

[assembly: InternalsVisibleTo("TestHelpers")]
namespace TreniniDotNet.Domain.Catalog.Scales
{
    /// <summary>
    /// A model trains <em>Scale</em> is the relationship between its size and the size of an actual train,
    /// usually measured as a ratio or as a millimetre to inch conversion. OO scale is said to be 4mm:ft or 1:76.
    ///
    /// A model trains <em>Gauge</em> is the distance between the inner edges of the two rails that it runs on.
    /// </summary>
    /// <seealso cref="Gauge"/>
    /// <seealso cref="Ratio"/>
    public sealed class Scale : AggregateRoot<ScaleId>, IScale
    {
        internal Scale(
            ScaleId id,
            string name,
            Slug slug,
            Ratio ratio,
            ScaleGauge gauge,
            string? description,
            IImmutableSet<ScaleStandard> standards,
            int? weight,
            Instant created,
            Instant? modified,
            int version)
            : base(id, created, modified, version)
        {
            Slug = slug;
            Name = name;
            Ratio = ratio;
            Gauge = gauge;
            Description = description;
            Standards = standards;
            Weight = weight;
        }

        #region [ Properties ]
        public Slug Slug { get; }

        public string Name { get; }

        public Ratio Ratio { get; }

        public ScaleGauge Gauge { get; }

        public string? Description { get; }

        public int? Weight { get; }

        public IImmutableSet<ScaleStandard> Standards { get; }
        #endregion

        public override string ToString() => $"{Name} ({Ratio})";

        public IScaleInfo ToScaleInfo() => this;
    }
}
