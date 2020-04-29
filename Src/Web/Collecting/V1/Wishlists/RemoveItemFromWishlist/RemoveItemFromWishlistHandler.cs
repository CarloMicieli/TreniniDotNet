using AutoMapper;
using TreniniDotNet.Application.Collecting.Wishlists.RemoveItemFromWishlist;
using TreniniDotNet.Web.Infrastructure.UseCases;

namespace TreniniDotNet.Web.Collecting.V1.Wishlists.RemoveItemFromWishlist
{
    public sealed class RemoveItemFromWishlistHandler : UseCaseHandler<IRemoveItemFromWishlistUseCase, RemoveItemFromWishlistRequest, RemoveItemFromWishlistInput>
    {
        public RemoveItemFromWishlistHandler(IRemoveItemFromWishlistUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}
