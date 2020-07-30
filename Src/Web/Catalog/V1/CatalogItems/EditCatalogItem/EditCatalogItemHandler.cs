using AutoMapper;
using TreniniDotNet.Application.Catalog.CatalogItems.EditCatalogItem;
using TreniniDotNet.Web.Infrastructure.UseCases;

namespace TreniniDotNet.Web.Catalog.V1.CatalogItems.EditCatalogItem
{
    public sealed class EditCatalogItemHandler : UseCaseHandler<EditCatalogItemUseCase, EditCatalogItemRequest, EditCatalogItemInput>
    {
        public EditCatalogItemHandler(EditCatalogItemUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}
