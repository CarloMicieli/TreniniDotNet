using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using TreniniDotNet.Common.Data;
using TreniniDotNet.Common.UseCases;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.CatalogItems.RollingStocks;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.SharedKernel.DeliveryDates;
using TreniniDotNet.SharedKernel.Slugs;
using static TreniniDotNet.Common.Enums.EnumHelpers;

namespace TreniniDotNet.Application.Catalog.CatalogItems.EditCatalogItem
{
    public sealed class EditCatalogItemUseCase : AbstractUseCase<EditCatalogItemInput, EditCatalogItemOutput, IEditCatalogItemOutputPort>
    {
        private readonly CatalogItemsService _catalogItemService;
        private readonly RollingStocksFactory _rollingStocksFactory;
        private readonly IUnitOfWork _unitOfWork;

        public EditCatalogItemUseCase(
            IUseCaseInputValidator<EditCatalogItemInput> inputValidator,
            IEditCatalogItemOutputPort outputPort,
            RollingStocksFactory rollingStocksFactory,
            CatalogItemsService catalogItemService,
            IUnitOfWork unitOfWork)
            : base(inputValidator, outputPort)
        {
            _catalogItemService = catalogItemService ??
                throw new ArgumentNullException(nameof(catalogItemService));
            _unitOfWork = unitOfWork ??
                throw new ArgumentNullException(nameof(unitOfWork));
            _rollingStocksFactory = rollingStocksFactory ??
                                    throw new ArgumentNullException(nameof(rollingStocksFactory));
        }

        protected override async Task Handle(EditCatalogItemInput input)
        {
            var item = await _catalogItemService.GetBySlugAsync(input.Slug);
            if (item is null)
            {
                OutputPort.CatalogItemNotFound(input.Slug);
                return;
            }

            Brand? brand = null;
            if (input.Values.Brand != null)
            {
                var brandSlug = Slug.Of(input.Values.Brand);
                brand = await _catalogItemService.FindBrandBySlug(brandSlug);
                if (brand is null)
                {
                    OutputPort.BrandNotFound(brandSlug);
                    return;
                }
            }

            Scale? scale = null;
            if (input.Values.Scale != null)
            {
                var scaleSlug = Slug.Of(input.Values.Scale);
                scale = await _catalogItemService.FindScaleBySlug(scaleSlug);
                if (scale is null)
                {
                    OutputPort.ScaleNotFound(scaleSlug);
                    return;
                }
            }

            IReadOnlyList<RollingStock> rollingStocks = ImmutableList<RollingStock>.Empty;
            if (input.Values.RollingStocks != null &&
                input.Values.RollingStocks.Count > 0)
            {
                IEnumerable<Slug> inputRailways = input.Values.RollingStocks
                    .Select(it => Slug.Of(it.Railway))
                    .Distinct();

                var (railways, railwaysNotFound) =
                    await _catalogItemService.FindRailwaysBySlug(inputRailways);

                if (railwaysNotFound.Count > 0)
                {
                    OutputPort.RailwayNotFound(railwaysNotFound);
                    return;
                }

                rollingStocks = input.Values.RollingStocks
                    .Select(it => _rollingStocksFactory.FromInput(it, railways))
                    .ToList();
            }
            else
            {
                rollingStocks = item.RollingStocks;
            }

            ItemNumber? itemNumber = null;
            if (input.Values.ItemNumber != null)
            {
                itemNumber = new ItemNumber(input.Values.ItemNumber);
            }

            var deliveryDate = input.Values.DeliveryDate.ToDeliveryDateOpt();
            var powerMethod = OptionalValueFor<PowerMethod>(input.Values.PowerMethod);

            var modifiedCatalogItem = item.With(
                BrandRef.AsOptional(brand),
                itemNumber,
                ScaleRef.AsOptional(scale),
                powerMethod,
                rollingStocks,
                input.Values.Description,
                input.Values.PrototypeDescription,
                input.Values.ModelDescription,
                deliveryDate,
                input.Values.Available
            );

            await _catalogItemService.UpdateCatalogItemAsync(modifiedCatalogItem);

            var _ = await _unitOfWork.SaveAsync();

            OutputPort.Standard(new EditCatalogItemOutput());
        }
    }
}
