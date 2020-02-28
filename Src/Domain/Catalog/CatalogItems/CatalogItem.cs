using System.Collections.Generic;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Common;

namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    public sealed class CatalogItem
    {
        private readonly CatalogItemId _id;
        private readonly IBrand _brand;
        private readonly Slug _slug;
        private readonly ItemNumber _itemNumber;
        private readonly IEnumerable<RollingStock> _rollingStocks;
        private readonly string _description;
        private readonly string? _prototypeDescr;
        private readonly string? _modelDescr;

        public CatalogItem(CatalogItemId id, 
            IBrand brand, 
            ItemNumber itemNumber, 
            Slug slug,
            IEnumerable<RollingStock> rollingStocks,
            string description, 
            string? prototypeDescr, 
            string? modelDescr)
        {
            _id = id;
            _brand = brand;
            _slug = slug;
            _itemNumber = itemNumber;
            _rollingStocks = rollingStocks;
            _description = description;
            _prototypeDescr = prototypeDescr;
            _modelDescr = modelDescr;
        }

        #region [ Properties ]
        public CatalogItemId CatalogItemId => _id;
        public IBrand Brand => _brand;
        public Slug Slug => _slug;
        public ItemNumber ItemNumber => _itemNumber;
        public IEnumerable<RollingStock> RollingStocks => _rollingStocks;
        public string Description => _description;
        public string? PrototypeDescription => _prototypeDescr;
        public string? ModelDescription => _modelDescr;
        #endregion
    }
}
