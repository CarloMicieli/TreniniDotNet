using AutoMapper;
using TreniniDotNet.Application.Boundaries.Collection.GetWishlistsByOwner;

namespace TreniniDotNet.Web.UseCases.V1.Collection.GetWishlistsByOwner
{
    public sealed class GetWishlistsByOwnerHandler : UseCaseHandler<IGetWishlistsByOwnerUseCase, GetWishlistsByOwnerRequest, GetWishlistsByOwnerInput>
    {
        public GetWishlistsByOwnerHandler(IGetWishlistsByOwnerUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}
