using System.Collections.Generic;
using System.Runtime.CompilerServices;
using NodaTime;
using TreniniDotNet.Common;
using TreniniDotNet.Common.DeliveryDates;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Domain.Catalog.ValueObjects;

[assembly: InternalsVisibleTo("TestHelpers")]
namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    public sealed class CatalogItem : AggregateRoot<CatalogItemId>, ICatalogItem
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
            : base(id, created, modified, version)
        {
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

        public override string ToString()
        {
            return $"CatalogItem({Brand.Name} {ItemNumber.Value})";
        }

        public ICatalogItemInfo ToCatalogItemInfo() => this;
    }
}
