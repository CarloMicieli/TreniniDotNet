using System;

namespace TreniniDotNet.Domain.Catalog.Brands
{
    public readonly struct BrandId : IEquatable<BrandId>
    {
        private Guid Id { get; }

        public static BrandId NewId()
        {
            return new BrandId(Guid.NewGuid());
        }

        public static BrandId Empty => new BrandId(Guid.Empty);

        public BrandId(Guid id)
        {
            Id = id;
        }

        public static implicit operator Guid(BrandId d) => d.Id;
        public static explicit operator BrandId(Guid guid) => new BrandId(guid);

        public override string ToString() => Id.ToString();

        public override int GetHashCode() => Id.GetHashCode();

        #region [ Equality ]
        public override bool Equals(object? obj)
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
            return left.Id == right.Id;
        }
        #endregion
    }
}