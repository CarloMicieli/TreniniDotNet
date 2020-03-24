using System;
using System.Collections.Generic;
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
        private readonly IScalesFactory _scalesFactory;

        public ScaleRepository(InMemoryContext context, IScalesFactory scalesFactory)
        {
            _context = context ??
                throw new ArgumentNullException(nameof(context));

            _scalesFactory = scalesFactory ??
                throw new ArgumentNullException(nameof(scalesFactory));
        }

        public Task<IScale> GetBySlug(Slug slug)
        {
            IScale scale = _context.Scales.FirstOrDefault(e => e.Slug == slug);
            return Task.FromResult(scale);
        }

        public Task<List<IScale>> GetAll()
        {
            return Task.FromResult(_context.Scales.ToList());
        }

        public Task<bool> Exists(Slug slug)
        {
            var found = _context.Scales.Any(e => e.Slug == slug);
            return Task.FromResult(found);
        }

        public Task<PaginatedResult<IScale>> GetScales(Page page)
        {
            var results = _context.Scales
                .OrderBy(r => r.Name)
                .Skip(page.Start)
                .Take(page.Limit + 1)
                .ToList();

            return Task.FromResult(new PaginatedResult<IScale>(page, results));
        }

        public Task<IScale> GetByName(string name)
        {
            IScale scale = _context.Scales
                .FirstOrDefault(e => e.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
            return Task.FromResult(scale);
        }

        public Task<ScaleId> Add(IScale scale)
        {
            _context.Scales.Add(scale);
            return Task.FromResult(scale.ScaleId);
        }
    }
}
