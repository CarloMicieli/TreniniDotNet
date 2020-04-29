using TreniniDotNet.Common.UseCases.Interfaces.Output;
using TreniniDotNet.Domain.Collecting.ValueObjects;

namespace TreniniDotNet.Application.Collecting.Wishlists.AddItemToWishlist
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
