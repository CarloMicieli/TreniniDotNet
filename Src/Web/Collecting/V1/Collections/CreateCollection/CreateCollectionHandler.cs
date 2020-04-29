using AutoMapper;
using TreniniDotNet.Application.Collecting.Collections.CreateCollection;
using TreniniDotNet.Web.Infrastructure.UseCases;

namespace TreniniDotNet.Web.Collecting.V1.Collections.CreateCollection
{
    public sealed class CreateCollectionHandler : UseCaseHandler<ICreateCollectionUseCase, CreateCollectionRequest, CreateCollectionInput>
    {
        public CreateCollectionHandler(ICreateCollectionUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}
