using NodaMoney;
using NodaTime;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.ValueObjects;

namespace TreniniDotNet.Domain.Collecting.Wishlists
{
    public interface IWishlistItem
    {
        WishlistItemId Id { get; }

        Priority Priority { get; }

        LocalDate AddedDate { get; }

        Money? Price { get; }

        ICatalogRef CatalogItem { get; }

        ICatalogItemDetails? Details { get; }

        string? Notes { get; }
    }
}
