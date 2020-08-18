using AutoMapper;
using TreniniDotNet.Application.Collecting.Wishlists.RemoveItemFromWishlist;
using TreniniDotNet.Web.Infrastructure.UseCases;

namespace TreniniDotNet.Web.Collecting.V1.Wishlists.RemoveItemFromWishlist
{
    public sealed class RemoveItemFromWishlistHandler : UseCaseHandler<RemoveItemFromWishlistUseCase, RemoveItemFromWishlistRequest, RemoveItemFromWishlistInput>
    {
        public RemoveItemFromWishlistHandler(RemoveItemFromWishlistUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}
