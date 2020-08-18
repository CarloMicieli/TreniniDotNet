using AutoMapper;
using TreniniDotNet.Application.Collecting.Wishlists.GetWishlistsByOwner;
using TreniniDotNet.Web.Infrastructure.UseCases;

namespace TreniniDotNet.Web.Collecting.V1.Wishlists.GetWishlistsByOwner
{
    public sealed class GetWishlistsByOwnerHandler : UseCaseHandler<GetWishlistsByOwnerUseCase, GetWishlistsByOwnerRequest, GetWishlistsByOwnerInput>
    {
        public GetWishlistsByOwnerHandler(GetWishlistsByOwnerUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}
