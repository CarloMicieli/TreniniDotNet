using System.Collections.Generic;
using System.Threading.Tasks;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.ValueObjects;

namespace TreniniDotNet.Domain.Collecting.Wishlists
{
    public interface IWishlistsRepository
    {
        Task<bool> ExistAsync(Owner owner, Slug wishlistSlug);

        Task<bool> ExistAsync(Owner owner, WishlistId id);

        Task<WishlistId> AddAsync(IWishlist wishList);

        Task DeleteAsync(WishlistId id);

        Task<IWishlist?> GetByIdAsync(WishlistId id);

        Task<IEnumerable<IWishlistInfo>> GetByOwnerAsync(Owner owner, VisibilityCriteria visibility);
    }
}
