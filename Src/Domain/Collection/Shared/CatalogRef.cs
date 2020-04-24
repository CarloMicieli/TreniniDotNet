using System;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Domain.Collection.Shared
{
    public sealed class CatalogRef : ICatalogRef, IEquatable<CatalogRef>
    {
        public CatalogRef(CatalogItemId catalogItemId, Slug slug)
        {
            CatalogItemId = catalogItemId;
            Slug = slug;
        }

        public static ICatalogRef Of(Guid catalogItemId, string slug) =>
            new CatalogRef(new CatalogItemId(catalogItemId), Slug.Of(slug));

        public static ICatalogRef From(ICatalogItemInfo info) =>
            new CatalogRef(info.CatalogItemId, info.Slug);

        public static ICatalogRef From(ICatalogItem info) =>
            new CatalogRef(info.CatalogItemId, info.Slug);

        public CatalogItemId CatalogItemId { get; }

        public Slug Slug { get; }

        #region [ Equality ]

        public static bool operator ==(CatalogRef left, CatalogRef right) => AreEquals(left, right);

        public static bool operator !=(CatalogRef left, CatalogRef right) => !AreEquals(left, right);

        public bool Equals(CatalogRef other) => AreEquals(this, other);

        public override bool Equals(object obj)
        {
            if (obj is CatalogRef that)
            {
                return AreEquals(this, that);
            }

            return false;
        }

        private static bool AreEquals(CatalogRef left, CatalogRef right) =>
            left.CatalogItemId == right.CatalogItemId &&
            left.Slug == right.Slug;

        #endregion

        public override int GetHashCode() => HashCode.Combine(CatalogItemId, Slug);

        public override string ToString() => $"Ref({CatalogItemId}, {Slug})";
    }
}
