using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.ValueObjects;
using TreniniDotNet.Domain.Collecting.Wishlists;
using TreniniDotNet.TestHelpers.InMemory.Repository;

namespace TreniniDotNet.Application.InMemory.Collecting.Wishlists
{
    public sealed class WishlistsRepository : IWishlistsRepository
    {
        private readonly InMemoryContext _context;

        public WishlistsRepository(InMemoryContext context)
        {
            _context = context;
        }

        public Task<WishlistId> AddAsync(IWishlist wishList)
        {
            return Task.FromResult(wishList.WishlistId);
        }

        public Task DeleteAsync(WishlistId id) =>
            Task.CompletedTask;

        public Task DeleteItemAsync(WishlistId id, WishlistItemId itemId)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> ExistAsync(Owner owner, Slug wishlistSlug)
        {
            var result = _context.WishLists
                .Any(it => it.Owner == owner && it.Slug == wishlistSlug);
            return Task.FromResult(result);
        }

        public Task<bool> ExistAsync(Owner owner, WishlistId id)
        {
            var result = _context.WishLists
                .Any(it => it.Owner == owner && it.WishlistId == id);
            return Task.FromResult(result);
        }

        public Task<IWishlist> GetByIdAsync(WishlistId id)
        {
            var result = _context.WishLists
                .FirstOrDefault(it => it.WishlistId == id);
            return Task.FromResult(result);
        }

        public Task<IEnumerable<IWishlistInfo>> GetByOwnerAsync(Owner owner, VisibilityCriteria visibility)
        {
            IEnumerable<IWishlistInfo> result = _context.WishLists
                .Where(it => it.Owner == owner);

            if (visibility != VisibilityCriteria.All)
            {
                var vis = (visibility == VisibilityCriteria.Private) ? Visibility.Private : Visibility.Public;
                result = result.Where(it => it.Visibility == vis);
            }

            result = result
                .Select(it => (IWishlistInfo)it)
                .ToList();
            return Task.FromResult(result);
        }
    }
}
