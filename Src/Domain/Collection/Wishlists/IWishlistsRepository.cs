using System.Collections.Generic;
using System.Threading.Tasks;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Collection.Shared;
using TreniniDotNet.Domain.Collection.ValueObjects;

namespace TreniniDotNet.Domain.Collection.Wishlists
{
    public interface IWishlistsRepository
    {
        Task<IEnumerable<IWishlistInfo>> GetWishListsByOwnerAsync(string owner);

        Task<IWishList> GetBySlugAsync(Slug slug);

        Task AddItemAsync(WishlistId id, IWishlistItem newItem);

        Task EditItemAsync(WishlistId id, IWishlistItem item);

        Task DeleteItemAsync(WishlistId id, WishlistItemId itemId);

        Task<bool> ExistAsync(Owner owner, Slug wishlistSlug);

        Task<bool> ExistAsync(WishlistId id);

        Task<WishlistId> AddAsync(IWishList wishList);
    }
}
