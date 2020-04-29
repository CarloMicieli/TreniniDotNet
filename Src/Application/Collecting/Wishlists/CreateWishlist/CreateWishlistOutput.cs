using TreniniDotNet.Common;
using TreniniDotNet.Common.UseCases.Interfaces.Output;
using TreniniDotNet.Domain.Collecting.ValueObjects;

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
