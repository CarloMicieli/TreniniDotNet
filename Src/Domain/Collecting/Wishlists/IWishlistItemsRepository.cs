using System.Threading.Tasks;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.ValueObjects;

namespace TreniniDotNet.Domain.Collecting.Wishlists
{
    public interface IWishlistItemsRepository
    {
        Task<WishlistItemId?> GetItemIdByCatalogRefAsync(WishlistId id, ICatalogRef catalogRef);

        Task<IWishlistItem?> GetItemByIdAsync(WishlistId id, WishlistItemId itemId);

        Task<WishlistItemId> AddItemAsync(
            WishlistId id,
            IWishlistItem newItem);

        Task EditItemAsync(WishlistId id, IWishlistItem modifiedItem);

        Task DeleteItemAsync(WishlistId id, WishlistItemId itemId);
    }
}
