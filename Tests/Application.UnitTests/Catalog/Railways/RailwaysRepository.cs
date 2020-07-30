using System.Linq;
using System.Threading.Tasks;
using TreniniDotNet.Common.Data.Pagination;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.SharedKernel.Slugs;
using TreniniDotNet.TestHelpers.InMemory.Repository;

namespace TreniniDotNet.Application.Catalog.Railways
{
    public sealed class RailwaysRepository : IRailwaysRepository
    {
        private InMemoryContext Context { get; }

        public RailwaysRepository(InMemoryContext context)
        {
            Context = context;
        }

        public Task<PaginatedResult<Railway>> GetAllAsync(Page page)
        {
            var results = Context.Railways
                .OrderBy(r => r.Name)
                .Skip(page.Start)
                .Take(page.Limit + 1)
                .ToList();

            return Task.FromResult(new PaginatedResult<Railway>(page, results));
        }

        public Task<RailwayId> AddAsync(Railway catalogItem)
        {
            Context.Railways.Add(catalogItem);
            return Task.FromResult(catalogItem.Id);
        }

        public Task UpdateAsync(Railway brand)
        {
            Context.Railways.RemoveAll(it => it.Id == brand.Id);
            Context.Railways.Add(brand);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(RailwayId id)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteAsync(Railway railway)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> ExistsAsync(Slug slug)
        {
            var found = Context.Railways.Any(e => e.Slug == slug);
            return Task.FromResult(found);
        }

        public Task<Railway> GetBySlugAsync(Slug slug)
        {
            var railway = Context.Railways.FirstOrDefault(e => e.Slug == slug);
            return Task.FromResult(railway);
        }
    }
}
