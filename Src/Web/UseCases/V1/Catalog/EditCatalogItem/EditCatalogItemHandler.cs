using AutoMapper;
using TreniniDotNet.Application.Boundaries.Catalog.EditCatalogItem;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.EditCatalogItem
{
    public sealed class EditCatalogItemHandler : UseCaseHandler<IEditCatalogItemUseCase, EditCatalogItemRequest, EditCatalogItemInput>
    {
        public EditCatalogItemHandler(IEditCatalogItemUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}
