using System.Linq;
using System.Threading.Tasks;
using TreniniDotNet.Common.Data.Pagination;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.SharedKernel.Slugs;
using TreniniDotNet.TestHelpers.InMemory.Repository;

namespace TreniniDotNet.Application.Catalog.Scales
{
    public sealed class ScalesRepository : IScalesRepository
    {
        private InMemoryContext Context { get; }

        public ScalesRepository(InMemoryContext context)
        {
            Context = context;
        }

        public Task<PaginatedResult<Scale>> GetAllAsync(Page page)
        {
            var results = Context.Scales
                .OrderBy(r => r.Name)
                .Skip(page.Start)
                .Take(page.Limit + 1)
                .ToList();

            return Task.FromResult(new PaginatedResult<Scale>(page, results));
        }

        public Task<ScaleId> AddAsync(Scale catalogItem)
        {
            Context.Scales.Add(catalogItem);
            return Task.FromResult(catalogItem.Id);
        }

        public Task UpdateAsync(Scale brand)
        {
            Context.Scales.RemoveAll(it => it.Id == brand.Id);
            Context.Scales.Add(brand);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(ScaleId id)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteAsync(Scale scale)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> ExistsAsync(Slug slug)
        {
            var found = Context.Scales.Any(e => e.Slug == slug);
            return Task.FromResult(found);
        }

        public Task<Scale> GetBySlugAsync(Slug slug)
        {
            var scale = Context.Scales.FirstOrDefault(e => e.Slug == slug);
            return Task.FromResult(scale);
        }
    }
}
