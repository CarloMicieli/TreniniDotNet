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
using TreniniDotNet.Common;
using TreniniDotNet.Application.Boundaries.Common;

namespace TreniniDotNet.Application.UseCases.Catalog.CatalogItems
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

            var rollingStocks = BuildRollingStocksList(input, railways);
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

        private IRollingStock ToRollingStock(RollingStockInput input, Dictionary<Slug, IRailwayInfo> railways)
        {
            if (railways.TryGetValue(Slug.Of(input.Railway), out var railwayInfo))
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
            Dictionary<Slug, IRailwayInfo> railways)
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
