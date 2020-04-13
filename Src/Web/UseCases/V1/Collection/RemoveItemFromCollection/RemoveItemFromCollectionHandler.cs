using AutoMapper;
using TreniniDotNet.Application.Boundaries.Collection.RemoveItemFromCollection;

namespace TreniniDotNet.Web.UseCases.V1.Collection.RemoveItemFromCollection
{
    public sealed class RemoveItemFromCollectionHandler : UseCaseHandler<IRemoveItemFromCollectionUseCase, RemoveItemFromCollectionRequest, RemoveItemFromCollectionInput>
    {
        public RemoveItemFromCollectionHandler(IRemoveItemFromCollectionUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}
