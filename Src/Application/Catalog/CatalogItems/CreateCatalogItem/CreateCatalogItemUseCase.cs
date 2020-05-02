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
        private readonly ICatalogItemsFactory _catalogItemsFactory;
        private readonly IRollingStocksFactory _rollingStocksFactory;

        public CreateCatalogItemUseCase(
            ICreateCatalogItemOutputPort outputPort,
            CatalogItemService catalogItemService,
            ICatalogItemsFactory catalogItemsFactory,
            IRollingStocksFactory rollingStocksFactory,
            IUnitOfWork unitOfWork)
            : base(new CreateCatalogItemInputValidator(), outputPort)
        {
            _unitOfWork = unitOfWork ??
                throw new ArgumentNullException(nameof(unitOfWork));
            _catalogItemService = catalogItemService ??
                throw new ArgumentNullException(nameof(catalogItemService));
            _catalogItemsFactory = catalogItemsFactory ??
                throw new ArgumentNullException(nameof(catalogItemsFactory));
            _rollingStocksFactory = rollingStocksFactory ??
                throw new ArgumentNullException(nameof(rollingStocksFactory));
        }

        protected override async Task Handle(CreateCatalogItemInput input)
        {
            var brandSlug = Slug.Of(input.Brand);
            IBrandInfo? brand = await _catalogItemService.FindBrandInfoBySlug(brandSlug);
            if (brand is null)
            {
                OutputPort.BrandNotFound(brandSlug);
                return;
            }

            var itemNumber = new ItemNumber(input.ItemNumber);
            bool exists = await _catalogItemService.ItemAlreadyExists(brand, itemNumber);
            if (exists)
            {
                OutputPort.CatalogItemAlreadyExists(brand, itemNumber);
                return;
            }

            var scaleSlug = Slug.Of(input.Scale);
            IScaleInfo? scale = await _catalogItemService.FindScaleInfoBySlug(scaleSlug);
            if (scale is null)
            {
                OutputPort.ScaleNotFound(scaleSlug);
                return;
            }

            var railwaysNotFound = new List<Slug>();
            var railways = new Dictionary<Slug, IRailwayInfo>();
            var inputRailways = input.RollingStocks
                .Select(rs => Slug.Of(rs.Railway))
                .Distinct();

            foreach (var railwaySlug in inputRailways)
            {
                IRailwayInfo? railwayInfo = await _catalogItemService.FindRailwayInfoBySlug(railwaySlug);
                if (railwayInfo is null)
                {
                    railwaysNotFound.Add(railwaySlug);
                }
                else
                {
                    railways.Add(railwaySlug, railwayInfo);
                }
            }

            if (railwaysNotFound.Count > 0)
            {
                OutputPort.RailwayNotFound(railwaysNotFound);
                return;
            }

            var rollingStocks = input.RollingStocks
                .Select(it => _rollingStocksFactory.FromInput(it, railways))
                .ToImmutableList();

            var deliveryDate = DeliveryDate.TryParse(input.DeliveryDate, out var dd) ? dd : null;

            var catalogItem = _catalogItemsFactory.CreateNewCatalogItem(
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

        private void CreateStandardOutput(ICatalogItem catalogItem)
        {
            var output = new CreateCatalogItemOutput(catalogItem.CatalogItemId, catalogItem.Slug);
            OutputPort.Standard(output);
        }
    }
}
