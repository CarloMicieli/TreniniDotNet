using AutoMapper;
using TreniniDotNet.Application.Catalog.CatalogItems.GetCatalogItemBySlug;
using TreniniDotNet.Web.Infrastructure.UseCases;

namespace TreniniDotNet.Web.Catalog.V1.CatalogItems.GetCatalogItemBySlug
{
    public sealed class GetCatalogItemBySlugHandler : UseCaseHandler<GetCatalogItemBySlugUseCase, GetCatalogItemBySlugRequest, GetCatalogItemBySlugInput>
    {
        public GetCatalogItemBySlugHandler(GetCatalogItemBySlugUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}