using System.Collections.Generic;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Scales;

namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    public sealed class CatalogItem : ICatalogItem
    {
        private readonly CatalogItemId _id;
        private readonly IBrand _brand;
        private readonly Slug _slug;
        private readonly IScale _scale;
        private readonly ItemNumber _itemNumber;
        private readonly IReadOnlyList<IRollingStock> _rollingStocks;
        private readonly string _description;
        private readonly string? _prototypeDescr;
        private readonly string? _modelDescr;
        private readonly PowerMethod _powerMethod;

        public CatalogItem(
            IBrand brand,
            ItemNumber itemNumber,
            IScale scale,
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

        public CatalogItem(CatalogItemId id,
            IBrand brand,
            ItemNumber itemNumber,
            Slug slug,
            IScale scale,
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
        }

        #region [ Properties ]
        public CatalogItemId CatalogItemId => _id;
        public IBrand Brand => _brand;
        public Slug Slug => _slug;
        public ItemNumber ItemNumber => _itemNumber;
        public IReadOnlyList<IRollingStock> RollingStocks => _rollingStocks;
        public string Description => _description;
        public string? PrototypeDescription => _prototypeDescr;
        public string? ModelDescription => _modelDescr;
        public IScale Scale => _scale;
        public PowerMethod PowerMethod => _powerMethod;
        #endregion

        private static Slug BuildSlug(IBrand brand, ItemNumber itemNumber)
        {
            return Slug.Of(brand.Name, itemNumber.Value);
        }
    }
}
