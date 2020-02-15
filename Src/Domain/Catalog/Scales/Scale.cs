using TreniniDotNet.Common;
using System;
using TreniniDotNet.Domain.Catalog.ValueObjects;

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
    public sealed class Scale : IEquatable<Scale>, IScale
    {
        private readonly ScaleId _id;
        private readonly Slug _slug;
        private readonly string _name;
        private readonly Ratio _ratio;
        private readonly Gauge _gauge;
        private readonly TrackGauge _trackGauge;
        private readonly string? _notes;

        public Scale(string name, Ratio ratio, Gauge gauge, TrackGauge trackGauge, string? notes)
            : this(ScaleId.NewId(), Slug.Empty, name, ratio, gauge, trackGauge, notes)
        { }

        public Scale(ScaleId id, Slug slug, string name, Ratio ratio, Gauge gauge, TrackGauge trackGauge, string? notes)
        {
            ValidateScaleName(name);

            _id = id;
            _slug = slug.OrNewIfEmpty(() => Slug.Of(name));
            _name = name;
            _ratio = ratio;
            _gauge = gauge;
            _notes = notes;
            _trackGauge = trackGauge;
        }

        #region [ Properties ]
        public ScaleId ScaleId => _id;

        public Slug Slug => _slug;

        public string Name => _name;

        public Ratio Ratio => _ratio;

        public Gauge Gauge => _gauge;

        public TrackGauge TrackGauge => _trackGauge;

        public string? Notes => _notes;
        #endregion

        #region [ Equality ]
        public static bool operator ==(Scale left, Scale right)
        {
            return AreEquals(left, right);
        }

        public static bool operator !=(Scale left, Scale right)
        {
            return !AreEquals(left, right);
        }

        public override bool Equals(object obj)
        {
            if (obj is Scale that)
            {
                return AreEquals(this, that);
            }

            return false;
        }

        public bool Equals(Scale other)
        {
            return AreEquals(this, other);
        }

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
        public override string ToString()
        {
            return $"{_name} ({_ratio})";
        }

        public override int GetHashCode()
        {
            return this._name.GetHashCode(StringComparison.InvariantCultureIgnoreCase);
        }
        #endregion

        private static void ValidateScaleName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException(ExceptionMessages.InvalidScaleName);
            }
        }
    }
}
