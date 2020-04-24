using AutoMapper;
using TreniniDotNet.Application.Boundaries.Collection.CreateCollection;

namespace TreniniDotNet.Web.UseCases.V1.Collection.CreateCollection
{
    public sealed class CreateCollectionHandler : UseCaseHandler<ICreateCollectionUseCase, CreateCollectionRequest, CreateCollectionInput>
    {
        public CreateCollectionHandler(ICreateCollectionUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}
