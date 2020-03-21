using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using LanguageExt;
using static LanguageExt.Prelude;
using TreniniDotNet.Application.Boundaries.Catalog.CreateCatalogItem;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Common;
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
        private readonly IRollingStocksFactory _rollingStocksFactory;

        public CreateCatalogItem(
            ICreateCatalogItemOutputPort outputPort,
            CatalogItemService catalogItemService,
            IRollingStocksFactory rollingStocksFactory,
            IUnitOfWork unitOfWork)
            : base(new CreateCatalogItemInputValidator(), outputPort)
        {
            _outputPort = outputPort;
            _unitOfWork = unitOfWork;
            _catalogItemService = catalogItemService;
            _rollingStocksFactory = rollingStocksFactory;
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

            Validation<Error, IEnumerable<IRollingStock>> result = input.RollingStocks
                .Select(it => ToRollingStock(it, railways))
                .Sequence();

            result.Match(
                Succ: async succ =>
                {
                    IReadOnlyList<IRollingStock> rollingStocks = succ.ToImmutableList();
                    var powerMethod = input.PowerMethod.ToPowerMethod() ?? PowerMethod.None;
                    var catalogItem = new CatalogItem(
                        brand,
                        itemNumber,
                        scale,
                        rollingStocks,
                        powerMethod,
                        input.Description,
                        input.PrototypeDescription,
                        input.ModelDescription);

                    var catalogItemId = await _catalogItemService.CreateNewCatalogItem(catalogItem);
                    await _unitOfWork.SaveAsync();

                    CreateStandardOutput(catalogItem);
                },
                Fail: errors =>
                {

                });
        }

        private Validation<Error, IRollingStock> ToRollingStock(RollingStockInput input, Dictionary<string, IRailway> railways)
        {
            IRailway? railway = null;
            if (railways.TryGetValue(input.Railway, out railway))
            {
                if (Categories.IsLocomotive(input.Category))
                {
                    return _rollingStocksFactory.NewLocomotive(
                        railway,
                        input.Era,
                        input.Category,
                        input.Length,
                        input.ClassName,
                        input.RoadNumber,
                        input.DccInterface,
                        input.Control
                    );

                }
                else
                {
                    return _rollingStocksFactory.NewRollingStock(
                        railway,
                        input.Era,
                        input.Category,
                        input.Length,
                        input.TypeName
                    );
                }
            }

            return Fail<Error, IRollingStock>(Error.New("invalid rolling stock: railway is not valid"));
        }

        private void CreateStandardOutput(CatalogItem catalogItem)
        {
            var output = new CreateCatalogItemOutput(catalogItem.CatalogItemId, catalogItem.Slug);
            OutputPort.Standard(output);
        }
    }
}
