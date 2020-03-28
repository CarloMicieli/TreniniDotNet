using System;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Domain.Catalog.CatalogItems
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

        public override int GetHashCode() => BrandId.GetHashCode();

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
            left.BrandId.Equals(right.BrandId);

        public IBrandInfo ToBrandInfo()
        {
            return this;
        }
    }
}
