using TreniniDotNet.Domain.Collection.Wishlists;

namespace TreniniDotNet.Application.Boundaries.Collection.GetWishlistById
{
    public sealed class GetWishlistByIdOutput : IUseCaseOutput
    {
        public GetWishlistByIdOutput(IWishlist wishlist)
        {
            Wishlist = wishlist;
        }

        public IWishlist Wishlist { get; }
    }
}
