using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using NodaTime;
using TreniniDotNet.Common.Domain;
using TreniniDotNet.SharedKernel.Slugs;

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
    public sealed class Scale : AggregateRoot<ScaleId>
    {
        private Scale()
        {
        }

        public Scale(
            ScaleId id,
            string name,
            Slug slug,
            Ratio ratio,
            ScaleGauge gauge,
            string? description,
            ISet<ScaleStandard> standards,
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
            Weight = weight;
            _standards = standards.ToHashSet();
        }

        #region [ Properties ]
        public Slug Slug { get; }

        public string Name { get; } = null!;

        public Ratio Ratio { get; }

        public ScaleGauge Gauge { get; } = null!;

        public string? Description { get; }

        public int? Weight { get; }

        private readonly HashSet<ScaleStandard> _standards = new HashSet<ScaleStandard>();
        public IImmutableSet<ScaleStandard> Standards => _standards.ToImmutableHashSet();
        #endregion

        public Scale With(
            string? name = null,
            Ratio? ratio = null,
            ScaleGauge? gauge = null,
            string? description = null,
            ISet<ScaleStandard>? standards = null,
            int? weight = null)
        {
            var slug = (name is null) ? Slug : Slug.Of(name);

            return new Scale(
                Id,
                name ?? Name,
                slug,
                ratio ?? Ratio,
                gauge ?? Gauge,
                description ?? Description,
                standards ?? _standards,
                weight ?? Weight,
                CreatedDate,
                ModifiedDate,
                Version);
        }

        public override string ToString() => $"{Name} ({Ratio})";
    }
}
