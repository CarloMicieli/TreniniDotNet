using AutoMapper;
using TreniniDotNet.Application.Collecting.Collections.AddItemToCollection;
using TreniniDotNet.Web.Infrastructure.UseCases;

namespace TreniniDotNet.Web.Collecting.V1.Collections.AddItemToCollection
{
    public sealed class AddItemToCollectionHandler : UseCaseHandler<IAddItemToCollectionUseCase, AddItemToCollectionRequest, AddItemToCollectionInput>
    {
        public AddItemToCollectionHandler(IAddItemToCollectionUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}
