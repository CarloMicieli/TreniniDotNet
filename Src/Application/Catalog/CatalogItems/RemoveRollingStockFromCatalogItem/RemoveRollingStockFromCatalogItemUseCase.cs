using System;
using System.Linq;
using System.Threading.Tasks;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Common.UseCases;
using TreniniDotNet.Domain.Catalog.CatalogItems;

namespace TreniniDotNet.Application.Catalog.CatalogItems.RemoveRollingStockFromCatalogItem
{
    public sealed class RemoveRollingStockFromCatalogItemUseCase : ValidatedUseCase<RemoveRollingStockFromCatalogItemInput, IRemoveRollingStockFromCatalogItemOutputPort>, IRemoveRollingStockFromCatalogItemUseCase
    {
        private readonly CatalogItemService _catalogItemService;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveRollingStockFromCatalogItemUseCase(IRemoveRollingStockFromCatalogItemOutputPort output,
            CatalogItemService catalogItemService,
            IUnitOfWork unitOfWork)
            : base(new RemoveRollingStockFromCatalogItemInputValidator(), output)
        {
            _catalogItemService = catalogItemService ??
                throw new ArgumentNullException(nameof(catalogItemService));
            _unitOfWork = unitOfWork ??
                throw new ArgumentNullException(nameof(unitOfWork));
        }

        protected override async Task Handle(RemoveRollingStockFromCatalogItemInput input)
        {
            var catalogItem = await _catalogItemService.GetBySlugAsync(input.CatalogItemSlug);
            if (catalogItem is null || catalogItem.RollingStocks.All(it => it.RollingStockId != input.RollingStockId))
            {
                OutputPort.RollingStockWasNotFound(input.CatalogItemSlug, input.RollingStockId);
                return;
            }

            await _catalogItemService.DeleteRollingStockAsync(catalogItem, input.RollingStockId);
            var _ = await _unitOfWork.SaveAsync();

            OutputPort.Standard(new RemoveRollingStockFromCatalogItemOutput());
        }
    }
}
