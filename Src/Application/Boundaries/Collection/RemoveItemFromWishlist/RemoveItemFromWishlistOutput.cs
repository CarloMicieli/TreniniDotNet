using TreniniDotNet.Domain.Collection.ValueObjects;

namespace TreniniDotNet.Application.Boundaries.Collection.RemoveItemFromWishlist
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
