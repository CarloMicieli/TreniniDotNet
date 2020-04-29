using System.Linq;
using System.Threading.Tasks;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.ValueObjects;
using TreniniDotNet.Domain.Collecting.Wishlists;
using TreniniDotNet.TestHelpers.InMemory.Repository;

namespace TreniniDotNet.Application.InMemory.Collecting.Wishlists
{
    public class WishlistItemsRepository : IWishlistItemsRepository
    {
        private readonly InMemoryContext _context;

        public WishlistItemsRepository(InMemoryContext context)
        {
            _context = context;
        }

        public Task<WishlistItemId> AddItemAsync(WishlistId id, IWishlistItem newItem) =>
            Task.FromResult(newItem.ItemId);

        public Task DeleteItemAsync(WishlistId id, WishlistItemId itemId) =>
            Task.CompletedTask;

        public Task EditItemAsync(WishlistId id, IWishlistItem modifiedItem) =>
            Task.CompletedTask;

        public Task<WishlistItemId?> GetItemIdByCatalogRefAsync(WishlistId id, ICatalogRef catalogRef)
        {
            var item = _context.WishLists
                .Where(it => it.WishlistId == id)
                .SelectMany(it => it.Items)
                .FirstOrDefault(it => it.CatalogItem.Slug.Equals(catalogRef.Slug));

            WishlistItemId? itemId = item?.ItemId;

            return Task.FromResult(itemId);
        }

        public Task<IWishlistItem> GetItemByIdAsync(WishlistId id, WishlistItemId itemId)
        {
            var result = _context.WishLists
                .Where(it => it.WishlistId == id)
                .SelectMany(it => it.Items)
                .FirstOrDefault(it => it.ItemId == itemId);
            return Task.FromResult(result);
        }
    }
}
