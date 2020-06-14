using System;
using System.Linq;
using System.Threading.Tasks;
using TreniniDotNet.Common;
using TreniniDotNet.Common.Pagination;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.TestHelpers.InMemory.Repository;

namespace TreniniDotNet.Application.Catalog.Scales
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

        public Task<IScaleInfo> GetInfoBySlugAsync(Slug slug)
        {
            IScaleInfo scale = _context.Scales.FirstOrDefault(e => e.Slug == slug);
            return Task.FromResult(scale);
        }

        public Task<ScaleId> AddAsync(IScale scale)
        {
            _context.Scales.Add(scale);
            return Task.FromResult(scale.Id);
        }

        public Task UpdateAsync(IScale scale)
        {
            _context.Scales.RemoveAll(it => it.Id == scale.Id);
            _context.Scales.Add(scale);
            return Task.CompletedTask;
        }
    }
}
