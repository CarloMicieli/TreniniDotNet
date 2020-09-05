using System.Collections.Generic;
using System.Threading.Tasks;
using TreniniDotNet.Common.Data;
using TreniniDotNet.Domain.Collecting.Shared;

namespace TreniniDotNet.Domain.Collecting.Wishlists
{
    public interface IWishlistsRepository : IRepository<WishlistId, Wishlist>
    {
        Task<bool> ExistsAsync(WishlistId id);

        Task<Wishlist?> GetByIdAsync(WishlistId id);

        Task<List<Wishlist>> GetByOwnerAsync(Owner owner, VisibilityCriteria visibility);

        Task<bool> ExistsAsync(Owner owner, string listName);

        Task<int> CountWishlistsAsync(Owner owner);
    }
}
