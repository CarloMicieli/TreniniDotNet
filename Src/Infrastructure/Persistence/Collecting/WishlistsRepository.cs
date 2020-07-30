using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.Wishlists;

namespace TreniniDotNet.Infrastructure.Persistence.Collecting
{
    public sealed class WishlistsRepository : EfCoreRepository<WishlistId, Wishlist>, IWishlistsRepository
    {
        public WishlistsRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }

        public Task<bool> ExistsAsync(WishlistId id) =>
            DbContext.Wishlists.AnyAsync(it => it.Id == id);

        public Task<Wishlist?> GetByIdAsync(WishlistId id) =>
#pragma warning disable 8619
            WishlistsQuery()
                .FirstOrDefaultAsync(it => it.Id == id);
#pragma warning restore 8619

        public Task<List<Wishlist>> GetByOwnerAsync(Owner owner, VisibilityCriteria visibility) =>
            WishlistsQuery().Where(it => it.Owner == owner).ToListAsync();

        public Task<bool> ExistsAsync(Owner owner, string listName) =>
            DbContext.Wishlists.AnyAsync(it => it.Owner == owner && it.ListName == listName);

        public Task<int> CountWishlistsAsync(Owner owner) =>
            DbContext.Wishlists.CountAsync(it => it.Owner == owner);

        private IQueryable<Wishlist> WishlistsQuery() =>
            DbContext.Wishlists
                .Include(x => x.Items)
                .ThenInclude(x => x.CatalogItem)
                .ThenInclude(x => x.Brand)
                .Include(x => x.Items)
                .ThenInclude(x => x.CatalogItem)
                .ThenInclude(x => x.RollingStocks)
                .ThenInclude(it => it.Railway)
                .Include(x => x.Items)
                .ThenInclude(x => x.CatalogItem)
                .ThenInclude(ci => ci.Scale);
    }
}
