using AutoMapper;
using TreniniDotNet.Application.Catalog.CatalogItems.RemoveRollingStockFromCatalogItem;
using TreniniDotNet.Web.Infrastructure.UseCases;

namespace TreniniDotNet.Web.Catalog.V1.CatalogItems.RemoveRollingStockFromCatalogItem
{
    public sealed class RemoveRollingStockFromCatalogItemHandler : UseCaseHandler<IRemoveRollingStockFromCatalogItemUseCase, RemoveRollingStockFromCatalogItemRequest, RemoveRollingStockFromCatalogItemInput>
    {
        public RemoveRollingStockFromCatalogItemHandler(IRemoveRollingStockFromCatalogItemUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}
