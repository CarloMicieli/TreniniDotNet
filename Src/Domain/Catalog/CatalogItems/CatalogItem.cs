using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using NodaTime;
using TreniniDotNet.Common.Domain;
using TreniniDotNet.Domain.Catalog.CatalogItems.RollingStocks;
using TreniniDotNet.SharedKernel.DeliveryDates;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    public sealed class CatalogItem : AggregateRoot<CatalogItemId>
    {
        public CatalogItem(
            CatalogItemId id,
            BrandRef brand,
            ItemNumber itemNumber,
            ScaleRef scale,
            PowerMethod powerMethod,
            string description,
            string? prototypeDescription,
            string? modelDescription,
            DeliveryDate? deliveryDate,
            bool available,
            IEnumerable<RollingStock> rollingStocks,
            Instant created,
            Instant? modified,
            int version)
            : base(id, created, modified, version)
        {
            Brand = brand;
            Scale = scale;
            ItemNumber = itemNumber;
            _rollingStocks = rollingStocks.ToList();
            Description = description;
            PrototypeDescription = prototypeDescription;
            ModelDescription = modelDescription;
            PowerMethod = powerMethod;
            DeliveryDate = deliveryDate;
            IsAvailable = available;
            Slug = Slug.Of(brand.Slug).CombineWith(itemNumber);
        }

        #region [ Properties ]

        public BrandRef Brand { get; }
        public Slug Slug { get; }
        public ItemNumber ItemNumber { get; }

        private readonly List<RollingStock> _rollingStocks;
        public IReadOnlyList<RollingStock> RollingStocks => _rollingStocks.ToImmutableList();

        public string Description { get; }
        public string? PrototypeDescription { get; }
        public string? ModelDescription { get; }
        public ScaleRef Scale { get; }
        public PowerMethod PowerMethod { get; }
        public DeliveryDate? DeliveryDate { get; }
        public bool IsAvailable { get; }
        #endregion

        public CatalogItem With(
            BrandRef? brand = null,
            ItemNumber? itemNumber = null,
            ScaleRef? scale = null,
            PowerMethod? powerMethod = null,
            IEnumerable<RollingStock>? rollingStocks = null,
            string? description = null,
            string? prototypeDescription = null,
            string? modelDescription = null,
            DeliveryDate? deliveryDate = null,
            bool? available = null)
        {
            var rollingStocksList = _rollingStocks;
            if (rollingStocks != null)
            {
                rollingStocksList = rollingStocks.ToList();
            }

            return new CatalogItem(
                Id,
                brand ?? Brand,
                itemNumber ?? ItemNumber,
                scale ?? Scale,
                powerMethod ?? PowerMethod,
                description ?? Description,
                prototypeDescription ?? PrototypeDescription,
                modelDescription ?? ModelDescription,
                deliveryDate ?? DeliveryDate,
                available ?? IsAvailable,
                rollingStocksList,
                CreatedDate,
                ModifiedDate,
                Version);
        }

        public void AddRollingStock(RollingStock rs)
        {
            _rollingStocks.Add(rs);
        }

        public void UpdateRollingStock(RollingStock rs)
        {
            _rollingStocks.RemoveAll(it => it.Id == rs.Id);
            _rollingStocks.Add(rs);
        }

        public void RemoveRollingStock(RollingStockId id)
        {
            _rollingStocks.RemoveAll(it => it.Id == id);
        }

        public int Count => _rollingStocks.Count;

        public CatalogItemCategory Category =>
            CatalogItemCategories.FromCatalogItem(this);

        public override string ToString() => $"CatalogItem({Brand}, {ItemNumber})";
    }
}
