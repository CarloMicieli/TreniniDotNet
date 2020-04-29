using TreniniDotNet.Common.UseCases.Interfaces.Output;
using TreniniDotNet.Domain.Collecting.ValueObjects;

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
