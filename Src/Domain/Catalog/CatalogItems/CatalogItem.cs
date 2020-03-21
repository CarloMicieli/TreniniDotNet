using System.Collections.Generic;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Scales;
using NodaTime;
using System;

namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    public sealed class CatalogItem : ICatalogItem, IEquatable<CatalogItem>
    {
        [Obsolete]
        public CatalogItem(
            IBrandInfo brand,
            ItemNumber itemNumber,
            IScaleInfo scale,
            IReadOnlyList<IRollingStock> rollingStocks,
            PowerMethod powerMethod,
            string description,
            string? prototypeDescr,
            string? modelDescr)
            : this(
                CatalogItemId.NewId(),
                brand,
                itemNumber,
                BuildSlug(brand, itemNumber),
                scale,
                rollingStocks,
                powerMethod,
                description,
                prototypeDescr,
                modelDescr)
        {
        }

        [Obsolete]
        public CatalogItem(
            CatalogItemId id,
            IBrandInfo brand,
            ItemNumber itemNumber,
            Slug slug,
            IScaleInfo scale,
            IReadOnlyList<IRollingStock> rollingStocks,
            PowerMethod powerMethod,
            string description,
            string? prototypeDescr,
            string? modelDescr)
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
            DeliveryDate = null;
            LastModifiedAt = Instant.FromUtc(2020, 1, 1, 0, 0);
            Version = 1;
        }

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

        #region [ Equality ]
        public override bool Equals(object obj)
        {
            if (obj is CatalogItem that)
            {
                return this.Equals(that);
            }

            return false;
        }

        public bool Equals(CatalogItem other)
        {
            return this.CatalogItemId.Equals(other.CatalogItemId);
        }

        public static bool operator ==(CatalogItem left, CatalogItem right) => left.Equals(right);
        public static bool operator !=(CatalogItem left, CatalogItem right) => !left.Equals(right);
        #endregion

        public override int GetHashCode() => CatalogItemId.GetHashCode();

        [Obsolete]
        private static Slug BuildSlug(IBrandInfo brand, ItemNumber itemNumber)
        {
            return Slug.Of(brand.Name, itemNumber.Value);
        }
    }
}
