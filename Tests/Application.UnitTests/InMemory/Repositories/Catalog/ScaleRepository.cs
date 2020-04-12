using System;
using System.Linq;
using System.Threading.Tasks;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Domain.Pagination;

namespace TreniniDotNet.Application.InMemory.Repositories.Catalog
{
    public sealed class ScaleRepository : IScalesRepository
    {
        private readonly InMemoryContext _context;

        public ScaleRepository(InMemoryContext context)
        {
            _context = context ??
                throw new ArgumentNullException(nameof(context));
        }

        public Task<IScale> GetBySlugAsync(Slug slug)
        {
            IScale scale = _context.Scales.FirstOrDefault(e => e.Slug == slug);
            return Task.FromResult(scale);
        }

        public Task<bool> ExistsAsync(Slug slug)
        {
            var found = _context.Scales.Any(e => e.Slug == slug);
            return Task.FromResult(found);
        }

        public Task<PaginatedResult<IScale>> GetScalesAsync(Page page)
        {
            var results = _context.Scales
                .OrderBy(r => r.Name)
                .Skip(page.Start)
                .Take(page.Limit + 1)
                .ToList();

            return Task.FromResult(new PaginatedResult<IScale>(page, results));
        }

        public Task<ScaleId> AddAsync(IScale scale)
        {
            _context.Scales.Add(scale);
            return Task.FromResult(scale.ScaleId);
        }
    }
}
