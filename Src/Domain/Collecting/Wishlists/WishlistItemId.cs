using System;

namespace TreniniDotNet.Domain.Collecting.Wishlists
{
    public readonly struct WishlistItemId : IEquatable<WishlistItemId>
    {
        private Guid Value { get; }

        public WishlistItemId(Guid id)
        {
            Value = id;
        }

        public static WishlistItemId NewId()
        {
            return new WishlistItemId(Guid.NewGuid());
        }

        public static WishlistItemId Empty => new WishlistItemId(Guid.Empty);

        public static implicit operator Guid(WishlistItemId d) => d.Value;
        public static explicit operator WishlistItemId(Guid guid) => new WishlistItemId(guid);

        public Guid ToGuid() => Value;
        
        public override string ToString() => $"WishlistItemId({Value})";

        public override int GetHashCode() => Value.GetHashCode();

        #region [ Equality ]

        public override bool Equals(object? obj)
        {
            if (obj is WishlistItemId that)
            {
                return this == that;
            }

            return false;
        }

        public static bool operator ==(WishlistItemId left, WishlistItemId right) => left.Value == right.Value;

        public static bool operator !=(WishlistItemId left, WishlistItemId right) => !(left == right);

        public bool Equals(WishlistItemId other) => this == other;

        #endregion
        
    }
}
