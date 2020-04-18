using TreniniDotNet.Domain.Collection.ValueObjects;

namespace TreniniDotNet.Application.Boundaries.Collection.AddItemToWishlist
{
    public sealed class AddItemToWishlistOutput : IUseCaseOutput
    {
        public AddItemToWishlistOutput(WishlistId id, WishlistItemId itemId)
        {
            Id = id;
            ItemId = itemId;
        }

        public WishlistId Id { set; get; }
        public WishlistItemId ItemId { set; get; }
    }
}
