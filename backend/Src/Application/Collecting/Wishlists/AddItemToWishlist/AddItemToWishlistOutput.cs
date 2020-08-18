using TreniniDotNet.Common.UseCases.Boundaries.Outputs;
using TreniniDotNet.Domain.Collecting.Wishlists;

namespace TreniniDotNet.Application.Collecting.Wishlists.AddItemToWishlist
{
    public sealed class AddItemToWishlistOutput : IUseCaseOutput
    {
        public AddItemToWishlistOutput(WishlistId id, WishlistItemId itemId)
        {
            Id = id;
            ItemId = itemId;
        }

        public WishlistId Id { get; }
        public WishlistItemId ItemId { get; }
    }
}
