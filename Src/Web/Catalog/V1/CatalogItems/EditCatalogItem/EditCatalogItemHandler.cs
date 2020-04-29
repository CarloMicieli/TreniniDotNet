using AutoMapper;
using TreniniDotNet.Application.Catalog.CatalogItems.EditCatalogItem;
using TreniniDotNet.Web.Infrastructure.UseCases;

namespace TreniniDotNet.Web.Catalog.V1.CatalogItems.EditCatalogItem
{
    public sealed class EditCatalogItemHandler : UseCaseHandler<IEditCatalogItemUseCase, EditCatalogItemRequest, EditCatalogItemInput>
    {
        public EditCatalogItemHandler(IEditCatalogItemUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}
