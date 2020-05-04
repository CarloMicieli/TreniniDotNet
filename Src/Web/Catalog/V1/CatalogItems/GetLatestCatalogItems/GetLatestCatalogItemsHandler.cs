using AutoMapper;
using TreniniDotNet.Application.Catalog.CatalogItems.GetLatestCatalogItems;
using TreniniDotNet.Web.Infrastructure.UseCases;

namespace TreniniDotNet.Web.Catalog.V1.CatalogItems.GetLatestCatalogItems
{
    public sealed class GetLatestCatalogItemsHandler : UseCaseHandler<IGetLatestCatalogItemsUseCase, GetLatestCatalogItemsRequest, GetLatestCatalogItemsInput>
    {
        public GetLatestCatalogItemsHandler(IGetLatestCatalogItemsUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}
