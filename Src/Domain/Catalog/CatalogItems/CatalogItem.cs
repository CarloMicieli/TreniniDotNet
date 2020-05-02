using NodaTime;
using System;
using System.Collections.Generic;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Common;
using TreniniDotNet.Common.DeliveryDates;
using TreniniDotNet.Common.Entities;

namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    public sealed class CatalogItem : ModifiableEntity, IEquatable<CatalogItem>, ICatalogItem
    {
        internal CatalogItem(
            CatalogItemId id,
            IBrandInfo brand,
            ItemNumber itemNumber,
            Slug slug,
            IScaleInfo scale,
            PowerMethod powerMethod,
            string description,
            string? prototypeDescription,
            string? modelDescription,
            DeliveryDate? deliveryDate,
            bool available,
            IReadOnlyList<IRollingStock> rollingStocks,
            Instant created,
            Instant? modified,
            int version)
            : base(created, modified, version)
        {
            CatalogItemId = id;
            Brand = brand;
            Slug = slug;
            Scale = scale;
            ItemNumber = itemNumber;
            RollingStocks = rollingStocks;
            Description = description;
            PrototypeDescription = prototypeDescription;
            ModelDescription = modelDescription;
            PowerMethod = powerMethod;
            DeliveryDate = deliveryDate;
            IsAvailable = available;
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
            return $"CatalogItem({Brand.Name} {ItemNumber.Value})";
        }

        public ICatalogItemInfo ToCatalogItemInfo() => this;
    }
}
