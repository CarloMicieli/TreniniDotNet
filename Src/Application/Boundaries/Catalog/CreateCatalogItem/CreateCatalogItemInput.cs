using System.Collections.Generic;
using TreniniDotNet.Common.Interfaces;

namespace TreniniDotNet.Application.Boundaries.Catalog.CreateCatalogItem
{
    public sealed class CreateCatalogItemInput : IUseCaseInput
    {
        private readonly string _brandName;
        private readonly string _itemNumber;
        private readonly string _description;
        private readonly string? _prototypeDescription;
        private readonly string? _modelDescription;
        private readonly string _powerMethod;
        private readonly string _scale;
        private readonly IList<RollingStockInput> _rollingStocks;

        public CreateCatalogItemInput(
            string brandName,
            string itemNumber,
            string description,
            string? prototypeDescription,
            string? modelDescription,
            string powerMethod,
            string scale,
            IList<RollingStockInput> rollingStocks)
        {
            _brandName = brandName;
            _itemNumber = itemNumber;
            _description = description;
            _prototypeDescription = prototypeDescription;
            _modelDescription = modelDescription;
            _powerMethod = powerMethod;
            _scale = scale;
            _rollingStocks = rollingStocks;
        }

        public string BrandName => _brandName;

        public string ItemNumber => _itemNumber;

        public string Description => _description;

        public string? PrototypeDescription => _prototypeDescription;

        public string? ModelDescription => _modelDescription;

        public string PowerMethod => _powerMethod;

        public string Scale => _scale;

        public IList<RollingStockInput> RollingStocks => _rollingStocks;
    }

    public class RollingStockInput
    {
        private readonly string _era;
        private readonly string _category;
        private readonly string _railway;
        private readonly string? _className;
        private readonly string? _roadNumber;
        private readonly decimal? _length;

        public RollingStockInput(string era, string category, string railway, string? className, string? roadNumber, decimal? length)
        {
            _era = era;
            _category = category;
            _railway = railway;
            _className = className;
            _roadNumber = roadNumber;
            _length = length;
        }

        public string Era => _era;

        public string Category => _category;

        public string Railway => _railway;

        public string? ClassName => _className;

        public string? RoadNumber => _roadNumber;

        public decimal? Length => _length;
    }
}
