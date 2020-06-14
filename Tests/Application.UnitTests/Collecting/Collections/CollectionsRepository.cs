using System.Linq;
using System.Threading.Tasks;
using TreniniDotNet.Domain.Collecting.Collections;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.ValueObjects;
using TreniniDotNet.TestHelpers.InMemory.Repository;

namespace TreniniDotNet.Application.InMemory.Collecting.Collections
{
    public sealed class CollectionsRepository : ICollectionsRepository
    {
        private readonly InMemoryContext _context;

        public CollectionsRepository(InMemoryContext context)
        {
            _context = context;
        }

        public Task<bool> ExistsAsync(Owner owner)
        {
            var result = _context.Collections
                .Any(it => it.Owner == owner);
            return Task.FromResult(result);
        }

        public Task EditItemAsync(CollectionId id, ICollectionItem item)
        {
            return Task.CompletedTask;
        }

        public Task<bool> ExistsAsync(Owner owner, CollectionId id)
        {
            var result = _context.Collections.Any(it => it.Id == id && it.Owner == owner);
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
                result = collection.Id;
            }

            return Task.FromResult(result);
        }

        public Task<CollectionId> AddAsync(ICollection collection) =>
            Task.FromResult(collection.Id);
    }
}
