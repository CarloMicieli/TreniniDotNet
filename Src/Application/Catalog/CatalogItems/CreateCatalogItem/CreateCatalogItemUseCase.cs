using System;
using System.Linq;
using System.Threading.Tasks;
using TreniniDotNet.Common.Data;
using TreniniDotNet.Common.UseCases;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.CatalogItems.RollingStocks;
using TreniniDotNet.SharedKernel.DeliveryDates;
using TreniniDotNet.SharedKernel.Slugs;
using static TreniniDotNet.Common.Enums.EnumHelpers;

namespace TreniniDotNet.Application.Catalog.CatalogItems.CreateCatalogItem
{
    public sealed class CreateCatalogItemUseCase : AbstractUseCase<CreateCatalogItemInput, CreateCatalogItemOutput, ICreateCatalogItemOutputPort>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly CatalogItemsService _catalogItemService;
        private readonly RollingStocksFactory _rollingStocksFactory;

        public CreateCatalogItemUseCase(
            IUseCaseInputValidator<CreateCatalogItemInput> inputValidator,
            ICreateCatalogItemOutputPort outputPort,
            RollingStocksFactory rollingStocksFactory,
            CatalogItemsService catalogItemService,
            IUnitOfWork unitOfWork)
            : base(inputValidator, outputPort)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _catalogItemService = catalogItemService ?? throw new ArgumentNullException(nameof(catalogItemService));
            _rollingStocksFactory = rollingStocksFactory ?? throw new ArgumentNullException(nameof(rollingStocksFactory));
        }

        protected override async Task Handle(CreateCatalogItemInput input)
        {
            var brandSlug = Slug.Of(input.Brand);
            var brand = await _catalogItemService.FindBrandBySlug(brandSlug);
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
            var scale = await _catalogItemService.FindScaleBySlug(scaleSlug);
            if (scale is null)
            {
                OutputPort.ScaleNotFound(scaleSlug);
                return;
            }

            var inputRailways = input.RollingStocks
                .Select(rs => Slug.Of(rs.Railway))
                .Distinct();

            var (railways, railwaysNotFound) =
                await _catalogItemService.FindRailwaysBySlug(inputRailways);

            if (railwaysNotFound.Count > 0)
            {
                OutputPort.RailwayNotFound(railwaysNotFound);
                return;
            }

            var rollingStocks = input.RollingStocks
                .Select(it => _rollingStocksFactory.FromInput(it, railways))
                .ToList();

            var powerMethod = RequiredValueFor<PowerMethod>(input.PowerMethod);

            var deliveryDate = input.DeliveryDate.ToDeliveryDateOpt();

            var (newCatalogItemId, newSlug) = await _catalogItemService.CreateCatalogItem(
                brand,
                itemNumber,
                scale,
                powerMethod,
                input.Description,
                input.PrototypeDescription,
                input.ModelDescription,
                deliveryDate,
                input.Available,
                rollingStocks);
            await _unitOfWork.SaveAsync();

            var output = new CreateCatalogItemOutput(newCatalogItemId, newSlug);
            OutputPort.Standard(output);
        }
    }
}
