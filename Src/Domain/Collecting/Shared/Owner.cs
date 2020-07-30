using System;
using TreniniDotNet.Domain.Collecting.Wishlists;

namespace TreniniDotNet.Domain.Collecting.Shared
{
    public readonly struct Owner : IEquatable<Owner>
    {
        public string Value { get; }

        public Owner(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Input value for owner is empty or null", nameof(value));
            }

            Value = value;
        }

        public static implicit operator string(Owner o) => o.Value;

        public bool CanView(Wishlist wishlist) =>
            this == wishlist.Owner || wishlist.Visibility == Visibility.Public;

        public bool CanEdit(Wishlist wishlist) =>
            this == wishlist.Owner;

        #region [ Equality ]

        public bool Equals(Owner other) => this == other;

        public override bool Equals(object? obj)
        {
            if (obj is Owner that)
            {
                return this == that;
            }

            return false;
        }

        public static bool operator ==(Owner left, Owner right) => left.Value == right.Value;

        public static bool operator !=(Owner left, Owner right) => !(left == right);

        #endregion

        public override int GetHashCode() => Value.GetHashCode();

        public override string ToString() => $"Owner({Value})";
    }
}
