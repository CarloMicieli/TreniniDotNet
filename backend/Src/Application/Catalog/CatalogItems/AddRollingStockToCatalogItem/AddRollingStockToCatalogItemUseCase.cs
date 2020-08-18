using System;
using System.Threading.Tasks;
using TreniniDotNet.Common.Data;
using TreniniDotNet.Common.UseCases;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.CatalogItems.RollingStocks;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Application.Catalog.CatalogItems.AddRollingStockToCatalogItem
{
    public sealed class AddRollingStockToCatalogItemUseCase : AbstractUseCase<AddRollingStockToCatalogItemInput, AddRollingStockToCatalogItemOutput, IAddRollingStockToCatalogItemOutputPort>
    {
        private readonly CatalogItemsService _catalogItemsService;
        private readonly RollingStocksFactory _rollingStocksFactory;
        private readonly IUnitOfWork _unitOfWork;

        public AddRollingStockToCatalogItemUseCase(
            IUseCaseInputValidator<AddRollingStockToCatalogItemInput> inputValidator,
            IAddRollingStockToCatalogItemOutputPort outputPort,
            RollingStocksFactory rollingStocksFactory,
            CatalogItemsService catalogItemsService,
            IUnitOfWork unitOfWork)
            : base(inputValidator, outputPort)
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
            var railway = await _catalogItemsService.FindRailwayBySlug(railwaySlug);
            if (railway is null)
            {
                OutputPort.RailwayWasNotFound(railwaySlug);
                return;
            }

            var rollingStock = _rollingStocksFactory.FromInput(input.RollingStock, railway);
            catalogItem.AddRollingStock(rollingStock);

            await _catalogItemsService.UpdateCatalogItemAsync(catalogItem);

            var _ = await _unitOfWork.SaveAsync();

            OutputPort.Standard(new AddRollingStockToCatalogItemOutput(slug));
        }
    }
}
