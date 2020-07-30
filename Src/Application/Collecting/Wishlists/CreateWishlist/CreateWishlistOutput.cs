using TreniniDotNet.Common.UseCases.Boundaries.Outputs;
using TreniniDotNet.Domain.Collecting.Wishlists;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Application.Collecting.Wishlists.CreateWishlist
{
    public sealed class CreateWishlistOutput : IUseCaseOutput
    {
        public CreateWishlistOutput(WishlistId wishlistId, Slug slug)
        {
            WishlistId = wishlistId;
            Slug = slug;
        }

        public WishlistId WishlistId { get; }
        public Slug Slug { get; }
    }
}
