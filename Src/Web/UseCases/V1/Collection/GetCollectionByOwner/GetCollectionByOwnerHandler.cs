using AutoMapper;
using TreniniDotNet.Application.Boundaries.Collection.GetCollectionByOwner;

namespace TreniniDotNet.Web.UseCases.V1.Collection.GetCollectionByOwner
{
    public sealed class GetCollectionByOwnerHandler : UseCaseHandler<IGetCollectionByOwnerUseCase, GetCollectionByOwnerRequest, GetCollectionByOwnerInput>
    {
        public GetCollectionByOwnerHandler(IGetCollectionByOwnerUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}
