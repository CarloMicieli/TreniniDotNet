using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Collection.Shared;
using TreniniDotNet.Domain.Collection.ValueObjects;
using TreniniDotNet.Domain.Collection.Wishlists;

namespace TreniniDotNet.Application.InMemory.Repositories.Collection
{
    public sealed class WishlistsRepository : IWishlistsRepository
    {
        private readonly InMemoryContext _context;

        public WishlistsRepository(InMemoryContext context)
        {
            _context = context;
        }

        public Task<WishlistId> AddAsync(IWishList wishList)
        {
            return Task.FromResult(wishList.WishlistId);
        }

        public Task AddItemAsync(WishlistId id, IWishlistItem newItem)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteItemAsync(WishlistId id, WishlistItemId itemId)
        {
            throw new System.NotImplementedException();
        }

        public Task EditItemAsync(WishlistId id, IWishlistItem item)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> ExistAsync(Owner owner, Slug wishlistSlug)
        {
            var result = _context.WishLists
                .Any(it => it.Owner == owner && it.Slug == wishlistSlug);
            return Task.FromResult(result);
        }

        public Task<bool> ExistAsync(WishlistId id)
        {
            var result = _context.WishLists
                .Any(it => it.WishlistId == id);
            return Task.FromResult(result);
        }

        public Task<IWishList> GetBySlugAsync(Slug slug)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<IWishlistInfo>> GetWishListsByOwnerAsync(string owner)
        {
            throw new System.NotImplementedException();
        }
    }
}
