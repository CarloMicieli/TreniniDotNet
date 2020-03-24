using System;

namespace TreniniDotNet.Domain.Catalog.ValueObjects
{
    public readonly struct BrandId : IEquatable<BrandId>
    {
        private readonly Guid _id;

        public static BrandId NewId()
        {
            return new BrandId(Guid.NewGuid());
        }

        public static BrandId Empty => new BrandId(Guid.Empty);

        public BrandId(Guid id)
        {
            _id = id;
        }

        public Guid ToGuid() => _id;

        public override string ToString() => _id.ToString();

        public override int GetHashCode() => _id.GetHashCode();

        #region [ Equality ]
        public override bool Equals(object obj)
        {
            if (obj is BrandId that)
            {
                return AreEquals(this, that);
            }

            return false;
        }

        public static bool operator ==(BrandId left, BrandId right)
        {
            return AreEquals(left, right);
        }

        public static bool operator !=(BrandId left, BrandId right)
        {
            return !AreEquals(left, right);
        }

        public bool Equals(BrandId other)
        {
            return AreEquals(this, other);
        }

        private static bool AreEquals(BrandId left, BrandId right)
        {
            return left._id == right._id;
        }
        #endregion
    }
}
