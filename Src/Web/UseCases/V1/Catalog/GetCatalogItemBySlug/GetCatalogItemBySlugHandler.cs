using AutoMapper;
using TreniniDotNet.Application.Boundaries.Catalog.GetCatalogItemBySlug;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.GetCatalogItemBySlug
{
    public sealed class GetCatalogItemBySlugHandler : UseCaseHandler<IGetCatalogItemBySlugUseCase, GetCatalogItemBySlugRequest, GetCatalogItemBySlugInput>
    {
        public GetCatalogItemBySlugHandler(IGetCatalogItemBySlugUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}