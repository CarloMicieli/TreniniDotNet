using System.Collections.Generic;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Scales;
using NodaTime;
using System;

namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    public sealed class CatalogItem : ICatalogItem
    {
        private readonly CatalogItemId _id;
        private readonly IBrandInfo _brand;
        private readonly Slug _slug;
        private readonly IScaleInfo _scale;
        private readonly ItemNumber _itemNumber;
        private readonly IReadOnlyList<IRollingStock> _rollingStocks;
        private readonly string _description;
        private readonly string? _prototypeDescr;
        private readonly string? _modelDescr;
        private readonly PowerMethod _powerMethod;
        private readonly DeliveryDate? _deliveryDate;
        private readonly bool _available = false;
        private readonly Instant _lastModifiedAt;
        private readonly int _version;

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
            _id = id;
            _brand = brand;
            _slug = slug;
            _scale = scale;
            _itemNumber = itemNumber;
            _rollingStocks = rollingStocks;
            _description = description;
            _prototypeDescr = prototypeDescr;
            _modelDescr = modelDescr;
            _powerMethod = powerMethod;
            _deliveryDate = null;
            _lastModifiedAt = Instant.FromUtc(2020, 1, 1, 0, 0);
            _version = 1;
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
            int version
            )
        {
            _id = id;
            _brand = brand;
            _slug = slug;
            _scale = scale;
            _itemNumber = itemNumber;
            _rollingStocks = rollingStocks;
            _description = description;
            _prototypeDescr = prototypeDescr;
            _modelDescr = modelDescr;
            _powerMethod = powerMethod;
            _deliveryDate = deliveryDate;
            _available = available;
            _lastModifiedAt = lastModifiedAt;
            _version = version;
        }

        #region [ Properties ]
        public CatalogItemId CatalogItemId => _id;
        public IBrandInfo Brand => _brand;
        public Slug Slug => _slug;
        public ItemNumber ItemNumber => _itemNumber;
        public IReadOnlyList<IRollingStock> RollingStocks => _rollingStocks;
        public string Description => _description;
        public string? PrototypeDescription => _prototypeDescr;
        public string? ModelDescription => _modelDescr;
        public IScaleInfo Scale => _scale;
        public PowerMethod PowerMethod => _powerMethod;
        public DeliveryDate? DeliveryDate => _deliveryDate;
        public bool IsAvailable => _available;
        public int Version => _version;
        public Instant LastModifiedAt => _lastModifiedAt;
        #endregion

        private static Slug BuildSlug(IBrandInfo brand, ItemNumber itemNumber)
        {
            return Slug.Of(brand.Name, itemNumber.Value);
        }
    }
}
