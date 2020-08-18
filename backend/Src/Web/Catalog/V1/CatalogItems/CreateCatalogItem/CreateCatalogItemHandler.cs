using AutoMapper;
using TreniniDotNet.Application.Catalog.CatalogItems.CreateCatalogItem;
using TreniniDotNet.Web.Infrastructure.UseCases;

namespace TreniniDotNet.Web.Catalog.V1.CatalogItems.CreateCatalogItem
{
    public sealed class CreateCatalogItemHandler : UseCaseHandler<CreateCatalogItemUseCase, CreateCatalogItemRequest, CreateCatalogItemInput>
    {
        public CreateCatalogItemHandler(CreateCatalogItemUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}

