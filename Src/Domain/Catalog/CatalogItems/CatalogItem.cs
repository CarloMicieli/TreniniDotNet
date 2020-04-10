using System.Collections.Generic;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Scales;
using NodaTime;
using System;

namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    public sealed class CatalogItem : IEquatable<CatalogItem>, ICatalogItem
    {
        internal CatalogItem(
            CatalogItemId id,
            IBrandInfo brand,
            ItemNumber itemNumber,
            Slug slug,
            IScaleInfo scale,
            PowerMethod powerMethod,
            string description,
            string? prototypeDescr,
            string? modelDescr,
            DeliveryDate? deliveryDate,
            bool available,
            IReadOnlyList<IRollingStock> rollingStocks,
            Instant lastModifiedAt,
            int version)
        {
            CatalogItemId = id;
            Brand = brand;
            Slug = slug;
            Scale = scale;
            ItemNumber = itemNumber;
            RollingStocks = rollingStocks;
            Description = description;
            PrototypeDescription = prototypeDescr;
            ModelDescription = modelDescr;
            PowerMethod = powerMethod;
            DeliveryDate = deliveryDate;
            IsAvailable = available;
            LastModifiedAt = lastModifiedAt;
            Version = version;
        }

        #region [ Properties ]
        public CatalogItemId CatalogItemId { get; }
        public IBrandInfo Brand { get; }
        public Slug Slug { get; }
        public ItemNumber ItemNumber { get; }
        public IReadOnlyList<IRollingStock> RollingStocks { get; }
        public string Description { get; }
        public string? PrototypeDescription { get; }
        public string? ModelDescription { get; }
        public IScaleInfo Scale { get; }
        public PowerMethod PowerMethod { get; }
        public DeliveryDate? DeliveryDate { get; }
        public bool IsAvailable { get; }
        public int Version { get; }
        public Instant LastModifiedAt { get; }
        #endregion

        public static bool operator ==(CatalogItem left, CatalogItem right) => AreEquals(left, right);

        public static bool operator !=(CatalogItem left, CatalogItem right) => !AreEquals(left, right);

        public override bool Equals(object obj)
        {
            if (obj is CatalogItem other)
            {
                return AreEquals(this, other);
            }

            return false;
        }

        public bool Equals(CatalogItem other) => AreEquals(this, other);

        private static bool AreEquals(CatalogItem left, CatalogItem right) =>
            left.CatalogItemId == right.CatalogItemId;

        public override int GetHashCode() => HashCode.Combine(CatalogItemId);

        public override string ToString()
        {
            return $"CatalogItem({CatalogItemId} {Brand.Name} {ItemNumber})";
        }

        public ICatalogItemInfo ToCatalogItemInfo() => this;
    }
}
