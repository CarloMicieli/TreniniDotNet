using System;

namespace TreniniDotNet.Domain.Collecting.ValueObjects
{
    public readonly struct ShopId : IEquatable<ShopId>
    {
        private readonly Guid _id;

        public static ShopId NewId()
        {
            return new ShopId(Guid.NewGuid());
        }

        public static ShopId Empty => new ShopId(Guid.Empty);

        public ShopId(Guid id)
        {
            _id = id;
        }

        public Guid ToGuid() => _id;

        public override string ToString() => _id.ToString();

        public override int GetHashCode() => _id.GetHashCode();

        #region [ Equality ]
        public override bool Equals(object obj)
        {
            if (obj is ShopId that)
            {
                return AreEquals(this, that);
            }

            return false;
        }

        public static bool operator ==(ShopId left, ShopId right)
        {
            return AreEquals(left, right);
        }

        public static bool operator !=(ShopId left, ShopId right)
        {
            return !AreEquals(left, right);
        }

        public bool Equals(ShopId other)
        {
            return AreEquals(this, other);
        }

        private static bool AreEquals(ShopId left, ShopId right)
        {
            return left._id == right._id;
        }
        #endregion
    }
}
