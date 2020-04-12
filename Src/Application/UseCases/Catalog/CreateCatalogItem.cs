using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Catalog.CreateCatalogItem;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Common.DeliveryDates;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using System;
using static TreniniDotNet.Common.Enums.EnumHelpers;

namespace TreniniDotNet.Application.UseCases.Catalog
{
    public sealed class CreateCatalogItem : ValidatedUseCase<CreateCatalogItemInput, ICreateCatalogItemOutputPort>, ICreateCatalogItemUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly CatalogItemService _catalogItemService;
        private readonly ICatalogItemsFactory _catalogItemsFactory;

        public CreateCatalogItem(
            ICreateCatalogItemOutputPort outputPort,
            CatalogItemService catalogItemService,
            ICatalogItemsFactory catalogItemsFactory,
            IUnitOfWork unitOfWork)
            : base(new CreateCatalogItemInputValidator(), outputPort)
        {
            _unitOfWork = unitOfWork;
            _catalogItemService = catalogItemService;
            _catalogItemsFactory = catalogItemsFactory;
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
            var railways = new Dictionary<string, IRailwayInfo>();
            var railwayNames = input.RollingStocks
                .Select(rs => rs.Railway)
                .Distinct();

            foreach (var railwayName in railwayNames)
            {
                IRailwayInfo? railwayInfo = await _catalogItemService.FindRailwayByName(railwayName);
                if (railwayInfo is null)
                {
                    railwaysNotFound.Add(railwayName);
                }
                else
                {
                    railways.Add(railwayName, railwayInfo);
                }
            }

            if (railwaysNotFound.Count > 0)
            {
                OutputPort.RailwayNotFound("Any of the railway was not found", railwaysNotFound);
                return;
            }

            var rollingStocks = BuildRollingStocksList(input, railways);
            var deliveryDate = DeliveryDate.TryParse(input.DeliveryDate, out var dd) ? dd : null;

            var catalogItem = _catalogItemsFactory.NewCatalogItem(
                brand,
                itemNumber,
                scale,
                RequiredValueFor<PowerMethod>(input.PowerMethod),
                rollingStocks,
                input.Description,
                input.PrototypeDescription,
                input.ModelDescription,
                deliveryDate,
                input.Available);

            var _ = await _catalogItemService.CreateNewCatalogItem(catalogItem);
            await _unitOfWork.SaveAsync();

            CreateStandardOutput(catalogItem);
        }

        private IRollingStock ToRollingStock(RollingStockInput input, Dictionary<string, IRailwayInfo> railways)
        {
            if (railways.TryGetValue(input.Railway, out var railwayInfo))
            {
                var length = LengthOverBuffer.CreateOrDefault(input.Length?.Inches, input.Length?.Millimeters);

                if (Categories.IsLocomotive(input.Category))
                {
                    return _catalogItemsFactory.NewLocomotive(
                        railwayInfo,
                        input.Era,
                        input.Category,
                        length,
                        input.ClassName,
                        input.RoadNumber,
                        input.DccInterface,
                        input.Control
                    );
                }
                else
                {
                    return _catalogItemsFactory.NewRollingStock(
                        railwayInfo,
                        input.Era,
                        input.Category,
                        length,
                        input.TypeName
                    );
                }
            }

            throw new InvalidOperationException("FIX ME");
        }

        private IImmutableList<IRollingStock> BuildRollingStocksList(
            CreateCatalogItemInput input,
            Dictionary<string, IRailwayInfo> railways)
        {
            return input.RollingStocks
                .Select(it => ToRollingStock(it, railways))
                .ToImmutableList();
        }

        private void CreateStandardOutput(ICatalogItem catalogItem)
        {
            var output = new CreateCatalogItemOutput(catalogItem.CatalogItemId, catalogItem.Slug);
            OutputPort.Standard(output);
        }
    }
}
