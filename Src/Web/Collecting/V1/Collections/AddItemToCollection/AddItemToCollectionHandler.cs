using AutoMapper;
using TreniniDotNet.Application.Collecting.Collections.AddItemToCollection;
using TreniniDotNet.Web.Infrastructure.UseCases;

namespace TreniniDotNet.Web.Collecting.V1.Collections.AddItemToCollection
{
    public sealed class AddItemToCollectionHandler : UseCaseHandler<AddItemToCollectionUseCase, AddItemToCollectionRequest, AddItemToCollectionInput>
    {
        public AddItemToCollectionHandler(AddItemToCollectionUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}
