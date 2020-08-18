using System;

namespace TreniniDotNet.Domain.Collecting.Shops
{
    public readonly struct ShopId : IEquatable<ShopId>, IComparable<ShopId>
    {
        private Guid Value { get; }

        public ShopId(Guid id)
        {
            Value = id;
        }

        public static ShopId NewId()
        {
            return new ShopId(Guid.NewGuid());
        }

        public static ShopId Empty => new ShopId(Guid.Empty);

        public static implicit operator Guid(ShopId d) => d.Value;
        public static explicit operator ShopId(Guid guid) => new ShopId(guid);

        public Guid ToGuid() => Value;

        public override string ToString() => $"ShopId({Value})";

        public override int GetHashCode() => Value.GetHashCode();

        #region [ Equality ]
        public override bool Equals(object? obj)
        {
            if (obj is ShopId that)
            {
                return this == that;
            }

            return false;
        }

        public static bool operator ==(ShopId left, ShopId right) =>
            left.Value == right.Value;

        public static bool operator !=(ShopId left, ShopId right) => !(left == right);

        public bool Equals(ShopId other) => this == other;
        #endregion

        public int CompareTo(ShopId other)
        {
            return Value.CompareTo(other.Value);
        }
    }
}
