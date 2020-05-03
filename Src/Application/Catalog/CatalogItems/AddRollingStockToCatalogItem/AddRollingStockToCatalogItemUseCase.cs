using System;
using System.Threading.Tasks;
using TreniniDotNet.Application.Catalog.CatalogItems.CreateCatalogItem;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Common;
using TreniniDotNet.Common.UseCases;
using TreniniDotNet.Domain.Catalog.CatalogItems;

namespace TreniniDotNet.Application.Catalog.CatalogItems.AddRollingStockToCatalogItem
{
    public sealed class AddRollingStockToCatalogItemUseCase : ValidatedUseCase<AddRollingStockToCatalogItemInput, IAddRollingStockToCatalogItemOutputPort>, IAddRollingStockToCatalogItemUseCase
    {
        private readonly CatalogItemService _catalogItemsService;
        private readonly IRollingStocksFactory _rollingStocksFactory;
        private readonly IUnitOfWork _unitOfWork;

        public AddRollingStockToCatalogItemUseCase(IAddRollingStockToCatalogItemOutputPort output,
            CatalogItemService catalogItemsService,
            IRollingStocksFactory rollingStocksFactory,
            IUnitOfWork unitOfWork)
            : base(new AddRollingStockToCatalogItemInputValidator(), output)
        {
            _catalogItemsService = catalogItemsService ??
                throw new ArgumentNullException(nameof(catalogItemsService));
            _rollingStocksFactory = rollingStocksFactory ??
                throw new ArgumentNullException(nameof(rollingStocksFactory));
            _unitOfWork = unitOfWork ??
                throw new ArgumentNullException(nameof(unitOfWork));
        }

        protected override async Task Handle(AddRollingStockToCatalogItemInput input)
        {
            var slug = Slug.Of(input.Slug);
            var catalogItem = await _catalogItemsService.GetBySlugAsync(slug);
            if (catalogItem is null)
            {
                OutputPort.CatalogItemWasNotFound(slug);
                return;
            }

            var railwaySlug = Slug.Of(input.RollingStock.Railway);
            var railwayInfo = await _catalogItemsService.FindRailwayInfoBySlug(railwaySlug);
            if (railwayInfo is null)
            {
                OutputPort.RailwayWasNotFound(railwaySlug);
                return;
            }

            var rollingStock = _rollingStocksFactory.FromInput(input.RollingStock, railwayInfo);

            await _catalogItemsService.AddRollingStockAsync(catalogItem.CatalogItemId, rollingStock);

            var _ = await _unitOfWork.SaveAsync();

            OutputPort.Standard(new AddRollingStockToCatalogItemOutput());
        }
    }
}
