using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using NodaTime;
using TreniniDotNet.Common.Domain;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.CatalogItems.RollingStocks;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.SharedKernel.DeliveryDates;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    public sealed class CatalogItem : AggregateRoot<CatalogItemId>
    {
        private CatalogItem() { }

        public CatalogItem(
            CatalogItemId id,
            Brand brand,
            ItemNumber itemNumber,
            Scale scale,
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
            Slug = brand.Slug.CombineWith(itemNumber);
        }

        #region [ Properties ]

        public Brand Brand { get; } = null!;
        public Slug Slug { get; }
        public ItemNumber ItemNumber { get; }

        private readonly List<RollingStock> _rollingStocks = new List<RollingStock>();
        public IReadOnlyList<RollingStock> RollingStocks => _rollingStocks.ToImmutableList();

        public string Description { get; } = null!;
        public string? PrototypeDescription { get; }
        public string? ModelDescription { get; }
        public Scale Scale { get; } = null!;
        public PowerMethod PowerMethod { get; }
        public DeliveryDate? DeliveryDate { get; }
        public bool IsAvailable { get; }
        #endregion

        public CatalogItem With(
            Brand? brand = null,
            ItemNumber? itemNumber = null,
            Scale? scale = null,
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

        public override string ToString() => $"CatalogItem({Brand.Name} {ItemNumber.Value})";
    }
}
