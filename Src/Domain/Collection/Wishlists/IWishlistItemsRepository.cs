using System.Threading.Tasks;
using TreniniDotNet.Domain.Collection.Shared;
using TreniniDotNet.Domain.Collection.ValueObjects;

namespace TreniniDotNet.Domain.Collection.Wishlists
{
    public interface IWishlistItemsRepository
    {
        Task<IWishlistItem> GetItemByCatalogRefAsync(WishlistId id, ICatalogRef catalogRef);

        Task<WishlistItemId> AddItemAsync(
            WishlistId id,
            IWishlistItem newItem);
    }
}
