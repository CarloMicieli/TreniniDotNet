using System.Collections.Generic;
using System.Threading.Tasks;
using TreniniDotNet.Common;
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

        public Task<IWishList> GetBySlugAsync(Slug slug)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<IWishListInfo>> GetWishListsByOwnerAsync(string owner)
        {
            throw new System.NotImplementedException();
        }
    }
}
