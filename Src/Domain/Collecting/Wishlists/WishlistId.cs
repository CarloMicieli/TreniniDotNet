using System;

namespace TreniniDotNet.Domain.Collecting.Wishlists
{
    public readonly struct WishlistId : IEquatable<WishlistId>, IComparable<WishlistId>
    {
        private Guid Value { get; }

        public WishlistId(Guid id)
        {
            Value = id;
        }

        public static WishlistId NewId()
        {
            return new WishlistId(Guid.NewGuid());
        }

        public static WishlistId Empty => new WishlistId(Guid.Empty);

        public static implicit operator Guid(WishlistId d) => d.Value;
        public static explicit operator WishlistId(Guid guid) => new WishlistId(guid);

        public override string ToString() => Value.ToString();

        public override int GetHashCode() => Value.GetHashCode();

        #region [ Equality ]
        public override bool Equals(object? obj)
        {
            if (obj is WishlistId that)
            {
                return this == that;
            }

            return false;
        }

        public static bool operator ==(WishlistId left, WishlistId right) => left.Value == right.Value;

        public static bool operator !=(WishlistId left, WishlistId right) => !(left == right);

        public bool Equals(WishlistId other) => this == other;
        #endregion

        public int CompareTo(WishlistId other)
        {
            return Value.CompareTo(other.Value);
        }
    }
}
