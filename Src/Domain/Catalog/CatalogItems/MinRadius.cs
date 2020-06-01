using System;
using TreniniDotNet.Common.Lengths;

namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    /// <summary>
    /// Minimum curve radius for rolling stocks, in millimeters.
    /// </summary>
    public sealed class MinRadius : IEquatable<MinRadius>, IComparable<MinRadius>
    {
        private MinRadius(Length value)
        {
            Value = value;
        }

        public Length Value { get; }

        public decimal Millimeters => Value.Value;

        public static MinRadius OfMillimeters(decimal value)
        {
            var len = Length.OfMillimeters(value);
            return new MinRadius(len);
        }

        public static MinRadius? CreateOrDefault(decimal? minRadius)
        {
            if (minRadius.HasValue)
            {
                return MinRadius.OfMillimeters(minRadius.Value);
            }

            return null;
        }

        public override string ToString() => Value.ToString();

        public override int GetHashCode() => Value.GetHashCode();

        #region [ Equality / Comparable ]

        public override bool Equals(object obj)
        {
            if (obj is MinRadius other)
            {
                return AreEquals(this, other);
            }

            return false;
        }

        public bool Equals(MinRadius other) => AreEquals(this, other);

        public static bool operator ==(MinRadius left, MinRadius right) => AreEquals(left, right);

        public static bool operator !=(MinRadius left, MinRadius right) => !AreEquals(left, right);

        private static bool AreEquals(MinRadius left, MinRadius right) =>
            left.Value.Equals(right.Value);

        public int CompareTo(MinRadius other) => this.Value.CompareTo(other.Value);

        #endregion
    }
}
