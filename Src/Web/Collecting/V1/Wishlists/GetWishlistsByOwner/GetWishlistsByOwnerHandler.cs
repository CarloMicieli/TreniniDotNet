using AutoMapper;
using TreniniDotNet.Application.Collecting.Wishlists.GetWishlistsByOwner;
using TreniniDotNet.Web.Infrastructure.UseCases;

namespace TreniniDotNet.Web.Collecting.V1.Wishlists.GetWishlistsByOwner
{
    public sealed class GetWishlistsByOwnerHandler : UseCaseHandler<IGetWishlistsByOwnerUseCase, GetWishlistsByOwnerRequest, GetWishlistsByOwnerInput>
    {
        public GetWishlistsByOwnerHandler(IGetWishlistsByOwnerUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}
