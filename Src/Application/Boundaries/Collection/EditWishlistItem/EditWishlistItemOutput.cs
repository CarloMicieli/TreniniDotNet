using TreniniDotNet.Domain.Collection.ValueObjects;

namespace TreniniDotNet.Application.Boundaries.Collection.EditWishlistItem
{
    public sealed class EditWishlistItemOutput : IUseCaseOutput
    {
        public EditWishlistItemOutput(WishlistId id, WishlistItemId itemId)
        {
            Id = id;
            ItemId = itemId;
        }

        public WishlistId Id { get; }
        public WishlistItemId ItemId { get; }
    }
}
