using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Common;
using TreniniDotNet.Common.DeliveryDates;
using TreniniDotNet.Common.UseCases;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using static TreniniDotNet.Common.Enums.EnumHelpers;

namespace TreniniDotNet.Application.Catalog.CatalogItems.CreateCatalogItem
{
    public sealed class CreateCatalogItemUseCase : ValidatedUseCase<CreateCatalogItemInput, ICreateCatalogItemOutputPort>, ICreateCatalogItemUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly CatalogItemService _catalogItemService;
        private readonly IRollingStocksFactory _rollingStocksFactory;

        public CreateCatalogItemUseCase(
            ICreateCatalogItemOutputPort outputPort,
            CatalogItemService catalogItemService,
            IRollingStocksFactory rollingStocksFactory,
            IUnitOfWork unitOfWork)
            : base(new CreateCatalogItemInputValidator(), outputPort)
        {
            _unitOfWork = unitOfWork ??
                throw new ArgumentNullException(nameof(unitOfWork));
            _catalogItemService = catalogItemService ??
                throw new ArgumentNullException(nameof(catalogItemService));
            _rollingStocksFactory = rollingStocksFactory ??
                throw new ArgumentNullException(nameof(rollingStocksFactory));
        }

        protected override async Task Handle(CreateCatalogItemInput input)
        {
            var brandSlug = Slug.Of(input.Brand);
            var brand = await _catalogItemService.FindBrandInfoBySlug(brandSlug);
            if (brand is null)
            {
                OutputPort.BrandNotFound(brandSlug);
                return;
            }

            var itemNumber = new ItemNumber(input.ItemNumber);
            var exists = await _catalogItemService.ItemAlreadyExists(brand, itemNumber);
            if (exists)
            {
                OutputPort.CatalogItemAlreadyExists(brand, itemNumber);
                return;
            }

            var scaleSlug = Slug.Of(input.Scale);
            var scale = await _catalogItemService.FindScaleInfoBySlug(scaleSlug);
            if (scale is null)
            {
                OutputPort.ScaleNotFound(scaleSlug);
                return;
            }

            var inputRailways = input.RollingStocks
                .Select(rs => Slug.Of(rs.Railway))
                .Distinct();

            var (railways, railwaysNotFound) =
                await _catalogItemService.FindRailwaysInfoBySlug(inputRailways);

            if (railwaysNotFound.Count > 0)
            {
                OutputPort.RailwayNotFound(railwaysNotFound);
                return;
            }

            var rollingStocks = input.RollingStocks
                .Select(it => _rollingStocksFactory.FromInput(it, railways))
                .ToImmutableList();

            var powerMethod = RequiredValueFor<PowerMethod>(input.PowerMethod);

            var deliveryDate = input.DeliveryDate.ToDeliveryDateOpt();

            var (newCatalogItemId, newSlug) = await _catalogItemService.CreateNewCatalogItem(
                brand,
                itemNumber,
                scale,
                powerMethod,
                rollingStocks,
                input.Description,
                input.PrototypeDescription,
                input.ModelDescription,
                deliveryDate,
                input.Available);
            await _unitOfWork.SaveAsync();

            var output = new CreateCatalogItemOutput(newCatalogItemId, newSlug);
            OutputPort.Standard(output);
        }
    }
}
