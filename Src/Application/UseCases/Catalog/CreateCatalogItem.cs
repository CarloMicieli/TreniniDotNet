using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Catalog.CreateCatalogItem;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Application.UseCases.Catalog
{
    public sealed class CreateCatalogItem : ValidatedUseCase<CreateCatalogItemInput, ICreateCatalogItemOutputPort>, ICreateCatalogItemUseCase
    {
        private readonly ICreateCatalogItemOutputPort _outputPort;
        private readonly IUnitOfWork _unitOfWork;
        private readonly CatalogItemService _catalogItemService;

        public CreateCatalogItem(ICreateCatalogItemOutputPort outputPort, CatalogItemService catalogItemService, IUnitOfWork unitOfWork)
            : base(new CreateCatalogItemInputValidator(), outputPort)
        {
            _outputPort = outputPort;
            _unitOfWork = unitOfWork;
            _catalogItemService = catalogItemService;
        }

        protected override async Task Handle(CreateCatalogItemInput input)
        {
            IBrand? brand = await _catalogItemService.FindBrandByName(input.BrandName);
            if (brand is null)
            {
                OutputPort.BrandNameNotFound($"The brand with name '{input.BrandName}' was not found");
                return;
            }

            var itemNumber = new ItemNumber(input.ItemNumber);
            bool exists = await _catalogItemService.ItemAlreadyExists(brand, itemNumber);
            if (exists)
            {
                OutputPort.CatalogItemAlreadyExists($"The catalog item '{input.ItemNumber}' for '{input.BrandName}' already exists");
                return;
            }

            IScale? scale = await _catalogItemService.FindScaleByName(input.Scale);
            if (scale is null)
            {
                OutputPort.ScaleNotFound($"The scale '{input.Scale}' was not found");
                return;
            }

            var railwaysNotFound = new List<string>();
            var railways = new Dictionary<string, IRailway>();
            var railwayNames = input.RollingStocks
                .Select(rs => rs.Railway)
                .Distinct()
                .ToList();
            foreach (var railwayName in railwayNames)
            {
                IRailway? railway = await _catalogItemService.FindRailwayByName(railwayName);
                if (railway is null)
                {
                    railwaysNotFound.Add(railwayName);
                }
                else
                {
                    railways.Add(railwayName, railway);
                }
            }

            if (railwaysNotFound.Count > 0)
            {
                OutputPort.RailwayNotFound("Any of the railway was not found", railwaysNotFound);
                return;
            }

            var powerMethod = input.PowerMethod.ToPowerMethod() ?? PowerMethod.None;
            IEnumerable<IRollingStock> rollingStocks = input.RollingStocks
                .Select(input => ToRollingStock(input, scale, powerMethod, railways));


            return;
        }

        private IRollingStock ToRollingStock(RollingStockInput input, IScale scale, PowerMethod powerMethod, Dictionary<string, IRailway> railways)
        {
            IRailway? railway = null;
            if (railways.TryGetValue(input.Railway, out railway))
            {
                return new RollingStock(
                    railway, 
                    scale, 
                    input.Category.ToCategory() ?? Category.DieselLocomotive, //TODO
                    input.Era.ToEra() ?? Era.I, //TODO
                    powerMethod,
                    Length.Of(input.Length ?? 0M)
                );
            }

            throw new ApplicationException("FIXME");
        }
    }
}
/*
        private readonly string _brandName;
        private readonly string _itemNumber;
        private readonly string _description;
        private readonly string? _prototypeDescription;
        private readonly string? _modelDescription;
        private readonly string _powerMethod;
        private readonly string _scale;
        private readonly IList<RollingStockInput> _rollingStocks;

*/