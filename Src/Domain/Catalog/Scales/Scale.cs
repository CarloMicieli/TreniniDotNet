using TreniniDotNet.Common;
using System;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using NodaTime;
using System.Collections.Immutable;
using TreniniDotNet.Common.Entities;

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
    public sealed class Scale : ModifiableEntity, IEquatable<Scale>, IScale
    {
        internal Scale(ScaleId id,
            string name, Slug slug,
            Ratio ratio,
            ScaleGauge gauge,
            string? description,
            IImmutableSet<ScaleStandard> standards,
            int? weight,
            Instant created,
            Instant? modified,
            int version)
            : base(created, modified, version)
        {
            ScaleId = id;
            Slug = slug;
            Name = name;
            Ratio = ratio;
            Gauge = gauge;
            Description = description;
            Standards = standards;
            Weight = weight;
        }

        #region [ Properties ]

        public ScaleId ScaleId { get; }

        public Slug Slug { get; }

        public string Name { get; }

        public Ratio Ratio { get; }

        public ScaleGauge Gauge { get; }

        public string? Description { get; }

        public int? Weight { get; }

        public IImmutableSet<ScaleStandard> Standards { get; }
        #endregion

        #region [ Equality ]
        public static bool operator ==(Scale left, Scale right) => AreEquals(left, right);

        public static bool operator !=(Scale left, Scale right) => !AreEquals(left, right);

        public override bool Equals(object obj)
        {
            if (obj is Scale that)
            {
                return AreEquals(this, that);
            }

            return false;
        }

        public bool Equals(Scale other) => AreEquals(this, other);

        private static bool AreEquals(Scale left, Scale right)
        {
            if (ReferenceEquals(left, right))
            {
                return true;
            }

            return left.Name == right.Name;
        }
        #endregion

        #region [ Standard methods overrides ]
        public override string ToString() => $"{Name} ({Ratio})";

        public override int GetHashCode() => HashCode.Combine(Name);

        #endregion

        public IScaleInfo ToScaleInfo() => this;
    }
}
