using AutoMapper;
using TreniniDotNet.Application.Catalog.CatalogItems.AddRollingStockToCatalogItem;
using TreniniDotNet.Web.Infrastructure.UseCases;

namespace TreniniDotNet.Web.Catalog.V1.CatalogItems.AddRollingStockToCatalogItem
{
    public sealed class AddRollingStockToCatalogItemHandler : UseCaseHandler<IAddRollingStockToCatalogItemUseCase, AddRollingStockToCatalogItemRequest, AddRollingStockToCatalogItemInput>
    {
        public AddRollingStockToCatalogItemHandler(IAddRollingStockToCatalogItemUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}
