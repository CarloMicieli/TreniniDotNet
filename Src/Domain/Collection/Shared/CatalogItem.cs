using System;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Domain.Collection.Shared
{
    public sealed class CatalogItem : ICatalogItem, IEquatable<CatalogItem>
    {
        public CatalogItem(Slug slug, string brand, ItemNumber itemNumber)
        {
            Slug = slug;
            Brand = brand;
            ItemNumber = itemNumber;
        }

        public static CatalogItem Of(string brand, ItemNumber itemNumber) =>
            new CatalogItem(Slug.Of(brand).CombineWith(itemNumber), brand, itemNumber);

        public Slug Slug { get; }

        public string Brand { get; }

        public ItemNumber ItemNumber { get; }

        #region [ Equality ]

        public override int GetHashCode() => HashCode.Combine(Brand, ItemNumber, Slug);

        public static bool operator ==(CatalogItem left, CatalogItem right) => AreEquals(left, right);

        public static bool operator !=(CatalogItem left, CatalogItem right) => !AreEquals(left, right);

        public override bool Equals(object obj)
        {
            if (obj is CatalogItem that)
            {
                return AreEquals(this, that);
            }

            return false;
        }

        public bool Equals(CatalogItem other) => AreEquals(this, other);

        private static bool AreEquals(CatalogItem left, CatalogItem right) =>
            left.Brand == right.Brand &&
            left.ItemNumber == right.ItemNumber &&
            left.Slug == right.Slug;

        #endregion

        public override string ToString() =>
            $"Slug: {Slug}, Brand: {Brand}, Item Number: {ItemNumber}";
    }
}
