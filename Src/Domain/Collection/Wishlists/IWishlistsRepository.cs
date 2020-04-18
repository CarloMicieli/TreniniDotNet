using System.Collections.Generic;
using System.Threading.Tasks;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Collection.Shared;
using TreniniDotNet.Domain.Collection.ValueObjects;

namespace TreniniDotNet.Domain.Collection.Wishlists
{
    public interface IWishlistsRepository
    {
        Task<bool> ExistAsync(Owner owner, Slug wishlistSlug);

        Task<bool> ExistAsync(WishlistId id);

        Task<WishlistId> AddAsync(IWishlist wishList);

        Task DeleteAsync(WishlistId id);

        Task<IWishlist> GetByIdAsync(WishlistId id);

        Task<IEnumerable<IWishlistInfo>> GetByOwnerAsync(Owner owner, Visibility visibility);
    }
}
