using System.Linq;
using System.Threading.Tasks;
using TreniniDotNet.Domain.Collection.Collections;
using TreniniDotNet.Domain.Collection.Shared;
using TreniniDotNet.Domain.Collection.ValueObjects;

namespace TreniniDotNet.Application.InMemory.Repositories.Collection
{
    public sealed class CollectionsRepository : ICollectionsRepository
    {
        private readonly InMemoryContext _context;

        public CollectionsRepository(InMemoryContext context)
        {
            _context = context;
        }

        public Task<CollectionItemId> AddItemAsync(CollectionId id, ICollectionItem newItem)
        {
            return Task.FromResult(newItem.ItemId);
        }

        public Task<bool> AnyByOwnerAsync(string owner)
        {
            var result = _context.Collections
                .Any(it => it.Owner.Equals(owner, System.StringComparison.InvariantCultureIgnoreCase));
            return Task.FromResult(result);
        }

        public Task DeleteItemAsync(CollectionId id, CollectionItemId itemId)
        {
            throw new System.NotImplementedException();
        }

        public Task EditItemAsync(CollectionId id, ICollectionItem item)
        {
            return Task.CompletedTask;
        }

        public Task<bool> ExistsAsync(CollectionId id)
        {
            var result = _context.Collections.Any(it => it.CollectionId == id);
            return Task.FromResult(result);
        }

        public Task<ICollection> GetByOwnerAsync(Owner owner)
        {
            var result = _context.Collections
                .FirstOrDefault(it => it.Owner.Equals(owner.Value, System.StringComparison.InvariantCultureIgnoreCase));
            return Task.FromResult(result);
        }

        public Task<ICollectionItem> GetCollectionItemByIdAsync(CollectionId collectionId, CollectionItemId itemId)
        {
            var result = _context.Collections
                .Where(it => it.CollectionId == collectionId)
                .SelectMany(it => it.Items)
                .Where(it => it.ItemId == itemId)
                .FirstOrDefault();
            return Task.FromResult(result);
        }

        public Task<CollectionId?> GetIdByOwnerAsync(Owner owner)
        {
            var collection = _context.Collections
                .FirstOrDefault(it => it.Owner.Equals(owner, System.StringComparison.InvariantCultureIgnoreCase));

            CollectionId? result = null;
            if (collection is null)
            {
                result = null;
            }
            else
            {
                result = collection.CollectionId;
            }

            return Task.FromResult(result);
        }

        public Task<ICollectionStats> GetStatisticsAsync(string owner)
        {
            throw new System.NotImplementedException();
        }
    }
}
