using System.Threading.Tasks;
using TreniniDotNet.Domain.Collection.Collections;
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

        public Task AddItemAsync(CollectionId id, ICollectionItem newItem)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteItemAsync(CollectionId id, CollectionItemId itemId)
        {
            throw new System.NotImplementedException();
        }

        public Task EditItemAsync(CollectionId id, ICollectionItem item)
        {
            throw new System.NotImplementedException();
        }

        public Task<ICollection> GetByOwnerAsync(string owner)
        {
            throw new System.NotImplementedException();
        }

        public Task<ICollectionStats> GetStatisticsAsync(string owner)
        {
            throw new System.NotImplementedException();
        }
    }
}
