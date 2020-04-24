using TreniniDotNet.Common;
using TreniniDotNet.Domain.Collection.ValueObjects;

namespace TreniniDotNet.Application.Boundaries.Collection.CreateWishlist
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
