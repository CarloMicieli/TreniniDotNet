using AutoMapper;
using TreniniDotNet.Application.Boundaries.Catalog.CreateCatalogItem;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.CreateCatalogItem
{
    public sealed class CreateCatalogItemHandler : UseCaseHandler<ICreateCatalogItemUseCase, CreateCatalogItemRequest, CreateCatalogItemInput>
    {
        public CreateCatalogItemHandler(ICreateCatalogItemUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}

