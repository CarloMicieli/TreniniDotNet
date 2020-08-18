using TreniniDotNet.Common.UseCases.Boundaries.Outputs;
using TreniniDotNet.Domain.Collecting.Wishlists;

namespace TreniniDotNet.Application.Collecting.Wishlists.RemoveItemFromWishlist
{
    public sealed class RemoveItemFromWishlistOutput : IUseCaseOutput
    {
        public RemoveItemFromWishlistOutput(WishlistId id, WishlistItemId itemId)
        {
            Id = id;
            ItemId = itemId;
        }

        public WishlistId Id { get; }
        public WishlistItemId ItemId { get; }
    }
}
