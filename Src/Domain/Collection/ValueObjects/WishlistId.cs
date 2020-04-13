using System;

namespace TreniniDotNet.Domain.Collection.ValueObjects
{
    public readonly struct WishlistId : IEquatable<WishlistId>
    {
        private readonly Guid _id;

        public static WishlistId NewId()
        {
            return new WishlistId(Guid.NewGuid());
        }

        public static WishlistId Empty => new WishlistId(Guid.Empty);

        public WishlistId(Guid id)
        {
            _id = id;
        }

        public Guid ToGuid() => _id;

        public override string ToString() => _id.ToString();

        public override int GetHashCode() => _id.GetHashCode();

        #region [ Equality ]
        public override bool Equals(object obj)
        {
            if (obj is WishlistId that)
            {
                return AreEquals(this, that);
            }

            return false;
        }

        public static bool operator ==(WishlistId left, WishlistId right)
        {
            return AreEquals(left, right);
        }

        public static bool operator !=(WishlistId left, WishlistId right)
        {
            return !AreEquals(left, right);
        }

        public bool Equals(WishlistId other)
        {
            return AreEquals(this, other);
        }

        private static bool AreEquals(WishlistId left, WishlistId right)
        {
            return left._id == right._id;
        }
        #endregion
    }
}
