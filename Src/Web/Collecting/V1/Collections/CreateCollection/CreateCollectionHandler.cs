using AutoMapper;
using TreniniDotNet.Application.Collecting.Collections.CreateCollection;
using TreniniDotNet.Web.Infrastructure.UseCases;

namespace TreniniDotNet.Web.Collecting.V1.Collections.CreateCollection
{
    public sealed class CreateCollectionHandler : UseCaseHandler<CreateCollectionUseCase, CreateCollectionRequest, CreateCollectionInput>
    {
        public CreateCollectionHandler(CreateCollectionUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}
