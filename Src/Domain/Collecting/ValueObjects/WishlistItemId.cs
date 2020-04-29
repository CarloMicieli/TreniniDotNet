using System;

namespace TreniniDotNet.Domain.Collecting.ValueObjects
{
    public readonly struct WishlistItemId : IEquatable<WishlistItemId>
    {
        private readonly Guid _id;

        public static WishlistItemId NewId()
        {
            return new WishlistItemId(Guid.NewGuid());
        }

        public static WishlistItemId Empty => new WishlistItemId(Guid.Empty);

        public WishlistItemId(Guid id)
        {
            _id = id;
        }

        public Guid ToGuid() => _id;

        public override string ToString() => _id.ToString();

        public override int GetHashCode() => _id.GetHashCode();

        #region [ Equality ]
        public override bool Equals(object obj)
        {
            if (obj is WishlistItemId that)
            {
                return AreEquals(this, that);
            }

            return false;
        }

        public static bool operator ==(WishlistItemId left, WishlistItemId right)
        {
            return AreEquals(left, right);
        }

        public static bool operator !=(WishlistItemId left, WishlistItemId right)
        {
            return !AreEquals(left, right);
        }

        public bool Equals(WishlistItemId other)
        {
            return AreEquals(this, other);
        }

        private static bool AreEquals(WishlistItemId left, WishlistItemId right)
        {
            return left._id == right._id;
        }
        #endregion
    }
}
