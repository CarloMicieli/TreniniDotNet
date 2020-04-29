using AutoMapper;
using TreniniDotNet.Application.Collecting.Collections.RemoveItemFromCollection;
using TreniniDotNet.Web.Infrastructure.UseCases;

namespace TreniniDotNet.Web.Collecting.V1.Collections.RemoveItemFromCollection
{
    public sealed class RemoveItemFromCollectionHandler : UseCaseHandler<IRemoveItemFromCollectionUseCase, RemoveItemFromCollectionRequest, RemoveItemFromCollectionInput>
    {
        public RemoveItemFromCollectionHandler(IRemoveItemFromCollectionUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}
