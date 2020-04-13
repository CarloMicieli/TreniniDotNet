using AutoMapper;
using TreniniDotNet.Application.Boundaries.Collection.EditCollectionItem;

namespace TreniniDotNet.Web.UseCases.V1.Collection.EditCollectionItem
{
    public sealed class EditCollectionItemHandler : UseCaseHandler<IEditCollectionItemUseCase, EditCollectionItemRequest, EditCollectionItemInput>
    {
        public EditCollectionItemHandler(IEditCollectionItemUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}
