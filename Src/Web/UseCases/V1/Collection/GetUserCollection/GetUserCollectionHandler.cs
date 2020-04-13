using AutoMapper;
using TreniniDotNet.Application.Boundaries.Collection.GetUserCollection;

namespace TreniniDotNet.Web.UseCases.V1.Collection.GetUserCollection
{
    public sealed class GetUserCollectionHandler : UseCaseHandler<IGetUserCollectionUseCase, GetUserCollectionRequest, GetUserCollectionInput>
    {
        public GetUserCollectionHandler(IGetUserCollectionUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}
