using AutoMapper;
using TreniniDotNet.Application.Collecting.Collections.EditCollectionItem;
using TreniniDotNet.Web.Infrastructure.UseCases;

namespace TreniniDotNet.Web.Collecting.V1.Collections.EditCollectionItem
{
    public sealed class EditCollectionItemHandler : UseCaseHandler<IEditCollectionItemUseCase, EditCollectionItemRequest, EditCollectionItemInput>
    {
        public EditCollectionItemHandler(IEditCollectionItemUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}
