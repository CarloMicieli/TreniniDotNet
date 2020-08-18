using System.Linq;
using System.Threading.Tasks;
using TreniniDotNet.Common.Data.Pagination;
using TreniniDotNet.Domain.Collecting.Collections;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.TestHelpers.InMemory.Repository;

namespace TreniniDotNet.Application.Collecting.Collections
{
    public sealed class CollectionsRepository : ICollectionsRepository
    {
        private InMemoryContext Context { get; }

        public CollectionsRepository(InMemoryContext context)
        {
            Context = context;
        }

        public Task<PaginatedResult<Collection>> GetAllAsync(Page page)
        {
            throw new System.NotImplementedException();
        }

        public Task<CollectionId> AddAsync(Collection catalogItem)
        {
            Context.Collections.Add(catalogItem);
            return Task.FromResult(catalogItem.Id);
        }

        public Task UpdateAsync(Collection brand)
        {
            Context.Collections.RemoveAll(it => it.Id == brand.Id);
            Context.Collections.Add(brand);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(CollectionId id)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteAsync(Collection collection)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> ExistsAsync(CollectionId id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Collection> GetByIdAsync(CollectionId id)
        {
            var collection = Context.Collections.FirstOrDefault(it => it.Id == id);
            return Task.FromResult(collection);
        }

        public Task<Collection> GetByOwnerAsync(Owner owner)
        {
            var result = Context.Collections
                .FirstOrDefault(it => it.Owner == owner);
            return Task.FromResult(result);
        }

        public Task<bool> ExistsAsync(Owner owner)
        {
            var result = Context.Collections
                .Any(it => it.Owner == owner);
            return Task.FromResult(result);
        }
    }
}
