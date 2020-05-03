using System;
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

        protected override Task Handle(RemoveRollingStockFromCatalogItemInput input)
        {
            throw new System.NotImplementedException();
        }
    }
}
