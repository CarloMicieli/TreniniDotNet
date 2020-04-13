using System.Collections.Generic;
using System.Threading.Tasks;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Collection.ValueObjects;

namespace TreniniDotNet.Domain.Collection.Wishlists
{
    public interface IWishlistsRepository
    {
        Task<IEnumerable<IWishListInfo>> GetWishListsByOwnerAsync(string owner);

        Task<IWishList> GetBySlugAsync(Slug slug);

        Task AddItemAsync(WishlistId id, IWishlistItem newItem);

        Task EditItemAsync(WishlistId id, IWishlistItem item);

        Task DeleteItemAsync(WishlistId id, WishlistItemId itemId);
    }
}
