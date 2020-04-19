using System.Threading.Tasks;
using TreniniDotNet.Domain.Collection.Shared;
using TreniniDotNet.Domain.Collection.ValueObjects;

namespace TreniniDotNet.Domain.Collection.Wishlists
{
    public interface IWishlistItemsRepository
    {
        Task<IWishlistItem?> GetItemByCatalogRefAsync(WishlistId id, ICatalogRef catalogRef);

        Task<IWishlistItem?> GetItemByIdAsync(WishlistId id, WishlistItemId itemId);

        Task<WishlistItemId> AddItemAsync(
            WishlistId id,
            IWishlistItem newItem);

        Task EditItemAsync(WishlistId id, IWishlistItem modifiedItem);

        Task DeleteItemAsync(WishlistId id, WishlistItemId itemId);
    }
}
