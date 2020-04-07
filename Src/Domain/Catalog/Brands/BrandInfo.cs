using System;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Domain.Catalog.Brands
{
    public sealed class BrandInfo : IBrandInfo, IEquatable<BrandInfo>
    {
        public BrandInfo(Guid brandId, string slug, string name)
            : this(new BrandId(brandId), Slug.Of(slug), name)
        {
        }

        public BrandInfo(BrandId brandId, Slug slug, string name)
        {
            BrandId = brandId;
            Slug = slug;
            Name = name;
        }

        public BrandId BrandId { get; }

        public Slug Slug { get; }

        public string Name { get; }

        public override int GetHashCode() => HashCode.Combine(BrandId, Name, Slug);

        #region [ Equality ]

        public static bool operator ==(BrandInfo left, BrandInfo right) =>
            AreEquals(left, right);

        public static bool operator !=(BrandInfo left, BrandInfo right) =>
            !AreEquals(left, right);

        public override bool Equals(object obj)
        {
            if (obj is BrandInfo that)
            {
                return AreEquals(this, that);
            }

            return false;
        }

        public bool Equals(BrandInfo other) =>
            AreEquals(this, other);

        private static bool AreEquals(BrandInfo left, BrandInfo right) =>
            left.BrandId.Equals(right.BrandId) &&
            left.Name.Equals(right.Name) &&
            left.Slug.Equals(right.Slug);

        #endregion
    }
}