using System;
using System.Linq;
using System.Threading.Tasks;
using TreniniDotNet.Common.Data;
using TreniniDotNet.Common.UseCases;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;
using TreniniDotNet.Domain.Catalog.CatalogItems;

namespace TreniniDotNet.Application.Catalog.CatalogItems.RemoveRollingStockFromCatalogItem
{
    public sealed class RemoveRollingStockFromCatalogItemUseCase : AbstractUseCase<RemoveRollingStockFromCatalogItemInput, RemoveRollingStockFromCatalogItemOutput, IRemoveRollingStockFromCatalogItemOutputPort>
    {
        private readonly CatalogItemsService _catalogItemService;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveRollingStockFromCatalogItemUseCase(
            IUseCaseInputValidator<RemoveRollingStockFromCatalogItemInput> inputValidator,
            IRemoveRollingStockFromCatalogItemOutputPort output,
            CatalogItemsService catalogItemService,
            IUnitOfWork unitOfWork)
            : base(inputValidator, output)
        {
            _catalogItemService = catalogItemService ??
                throw new ArgumentNullException(nameof(catalogItemService));
            _unitOfWork = unitOfWork ??
                throw new ArgumentNullException(nameof(unitOfWork));
        }

        protected override async Task Handle(RemoveRollingStockFromCatalogItemInput input)
        {
            var catalogItem = await _catalogItemService.GetBySlugAsync(input.CatalogItemSlug);
            if (catalogItem is null || catalogItem.RollingStocks.All(it => it.Id != input.RollingStockId))
            {
                OutputPort.RollingStockWasNotFound(input.CatalogItemSlug, input.RollingStockId);
                return;
            }

            catalogItem.RemoveRollingStock(input.RollingStockId);

            await _catalogItemService.UpdateCatalogItemAsync(catalogItem);

            var _ = await _unitOfWork.SaveAsync();

            OutputPort.Standard(new RemoveRollingStockFromCatalogItemOutput());
        }
    }
}
