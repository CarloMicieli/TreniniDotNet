using TreniniDotNet.Common.UseCases.Boundaries.Outputs;
using TreniniDotNet.Domain.Collecting.Wishlists;

namespace TreniniDotNet.Application.Collecting.Wishlists.GetWishlistById
{
    public sealed class GetWishlistByIdOutput : IUseCaseOutput
    {
        public GetWishlistByIdOutput(Wishlist wishlist)
        {
            Wishlist = wishlist;
        }

        public Wishlist Wishlist { get; }
    }
}
