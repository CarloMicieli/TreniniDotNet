using AutoMapper;
using TreniniDotNet.Application.Boundaries.Collection.AddItemToCollection;

namespace TreniniDotNet.Web.UseCases.V1.Collection.AddItemToCollection
{
    public sealed class AddItemToCollectionHandler : UseCaseHandler<IAddItemToCollectionUseCase, AddItemToCollectionRequest, AddItemToCollectionInput>
    {
        public AddItemToCollectionHandler(IAddItemToCollectionUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}
