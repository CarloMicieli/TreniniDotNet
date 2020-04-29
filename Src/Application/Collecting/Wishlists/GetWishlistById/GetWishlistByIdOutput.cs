using TreniniDotNet.Common.UseCases.Interfaces.Output;
using TreniniDotNet.Domain.Collecting.Wishlists;

namespace TreniniDotNet.Application.Collecting.Wishlists.GetWishlistById
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
