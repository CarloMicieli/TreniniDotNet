using NodaTime;
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

        public Task<bool> AnyByOwnerAsync(Owner owner)
        {
            var result = _context.Collections
                .Any(it => it.Owner == owner);
            return Task.FromResult(result);
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
                .FirstOrDefault(it => it.Owner == owner);
            return Task.FromResult(result);
        }

        public Task<CollectionId?> GetIdByOwnerAsync(Owner owner)
        {
            var collection = _context.Collections
                .FirstOrDefault(it => it.Owner == owner);

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
    }
}
