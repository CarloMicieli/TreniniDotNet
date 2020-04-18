using System.Linq;
using System.Threading.Tasks;
using TreniniDotNet.Domain.Collection.Shared;
using TreniniDotNet.Domain.Collection.ValueObjects;
using TreniniDotNet.Domain.Collection.Wishlists;

namespace TreniniDotNet.Application.InMemory.Repositories.Collection
{
    public class WishlistItemsRepository : IWishlistItemsRepository
    {
        private readonly InMemoryContext _context;

        public WishlistItemsRepository(InMemoryContext context)
        {
            _context = context;
        }

        public Task<WishlistItemId> AddItemAsync(WishlistId id, IWishlistItem newItem)
        {
            return Task.FromResult(newItem.ItemId);
        }

        public Task EditItemAsync(WishlistId id, IWishlistItem modifiedItem) =>
            Task.CompletedTask;

        public Task<IWishlistItem> GetItemByCatalogRefAsync(WishlistId id, ICatalogRef catalogRef)
        {
            var result = _context.WishLists
                .Where(it => it.WishlistId == id)
                .SelectMany(it => it.Items)
                .Where(it => it.CatalogItem.Slug.Equals(catalogRef.Slug))
                .FirstOrDefault();
            return Task.FromResult(result);
        }

        public Task<IWishlistItem> GetItemByIdAsync(WishlistId id, WishlistItemId itemId)
        {
            var result = _context.WishLists
                .Where(it => it.WishlistId == id)
                .SelectMany(it => it.Items)
                .Where(it => it.ItemId == itemId)
                .FirstOrDefault();
            return Task.FromResult(result);
        }
    }
}
