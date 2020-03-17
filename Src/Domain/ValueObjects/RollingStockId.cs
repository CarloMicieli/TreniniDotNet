using System;

namespace TreniniDotNet.Domain.Catalog.ValueObjects
{
    public readonly struct RollingStockId : IEquatable<RollingStockId>
    {
        private readonly Guid _id;

        public static RollingStockId NewId()
        {
            return new RollingStockId(Guid.NewGuid());
        }

        public static RollingStockId Empty => new RollingStockId(Guid.Empty);

        public RollingStockId(Guid id)
        {
            _id = id;
        }

        public Guid ToGuid() => _id;

        public override string ToString() => _id.ToString();

        public override int GetHashCode() => _id.GetHashCode();

        #region [ Equality ]
        public override bool Equals(object obj)
        {
            if (obj is RollingStockId that)
            {
                return AreEquals(this, that);
            }

            return false;
        }

        public static bool operator ==(RollingStockId left, RollingStockId right)
        {
            return AreEquals(left, right);
        }

        public static bool operator !=(RollingStockId left, RollingStockId right)
        {
            return !AreEquals(left, right);
        }

        public bool Equals(RollingStockId other)
        {
            return AreEquals(this, other);
        }

        private static bool AreEquals(RollingStockId left, RollingStockId right)
        {
            return left._id == right._id;
        }
        #endregion
    }
}
