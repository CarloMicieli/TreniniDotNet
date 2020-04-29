using AutoMapper;
using TreniniDotNet.Application.Catalog.CatalogItems.GetCatalogItemBySlug;
using TreniniDotNet.Web.Infrastructure.UseCases;

namespace TreniniDotNet.Web.Catalog.V1.CatalogItems.GetCatalogItemBySlug
{
    public sealed class GetCatalogItemBySlugHandler : UseCaseHandler<IGetCatalogItemBySlugUseCase, GetCatalogItemBySlugRequest, GetCatalogItemBySlugInput>
    {
        public GetCatalogItemBySlugHandler(IGetCatalogItemBySlugUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}