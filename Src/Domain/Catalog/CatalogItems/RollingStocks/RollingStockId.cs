using System;

namespace TreniniDotNet.Domain.Catalog.CatalogItems.RollingStocks
{
    public readonly struct RollingStockId : IEquatable<RollingStockId>, IComparable<RollingStockId>
    {
        private Guid Value { get; }

        public RollingStockId(Guid value)
        {
            Value = value;
        }

        public static RollingStockId NewId()
        {
            return new RollingStockId(Guid.NewGuid());
        }

        public static RollingStockId Empty => new RollingStockId(Guid.Empty);

        public static implicit operator Guid(RollingStockId id) { return id.Value; }

        public override string ToString() => Value.ToString();

        public override int GetHashCode() => Value.GetHashCode();

        #region [ Equality ]
        public override bool Equals(object? obj)
        {
            if (obj is RollingStockId that)
            {
                return this == that;
            }

            return false;
        }

        public static bool operator ==(RollingStockId left, RollingStockId right) =>
            left.Value == right.Value;

        public static bool operator !=(RollingStockId left, RollingStockId right) => !(left == right);

        public bool Equals(RollingStockId other) => this == other;

        #endregion

        public int CompareTo(RollingStockId other)
        {
            return Value.CompareTo(other.Value);
        }
    }
}
