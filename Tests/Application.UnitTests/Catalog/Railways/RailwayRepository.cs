using System.Linq;
using System.Threading.Tasks;
using TreniniDotNet.Common;
using TreniniDotNet.Common.Pagination;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.TestHelpers.InMemory.Repository;

namespace TreniniDotNet.Application.Catalog.Railways
{
    public sealed class RailwayRepository : IRailwaysRepository
    {
        private readonly InMemoryContext _context;

        public RailwayRepository(InMemoryContext context)
        {
            _context = context;
        }

        public Task<RailwayId> AddAsync(IRailway railway)
        {
            _context.Railways.Add(railway);
            return Task.FromResult(railway.RailwayId);
        }

        public Task<bool> ExistsAsync(Slug slug)
        {
            var found = _context.Railways.Any(e => e.Slug == slug);
            return Task.FromResult(found);
        }

        public Task<IRailway> GetBySlugAsync(Slug slug)
        {
            IRailway railway = _context.Railways.FirstOrDefault(e => e.Slug == slug);
            return Task.FromResult(railway);
        }

        public Task<IRailwayInfo> GetInfoBySlugAsync(Slug slug)
        {
            IRailwayInfo railway = _context.Railways.FirstOrDefault(e => e.Slug == slug);
            return Task.FromResult(railway);
        }

        public Task<PaginatedResult<IRailway>> GetRailwaysAsync(Page page)
        {
            var results = _context.Railways
                .OrderBy(r => r.Name)
                .Skip(page.Start)
                .Take(page.Limit + 1)
                .ToList();

            return Task.FromResult(new PaginatedResult<IRailway>(page, results));
        }

        public Task UpdateAsync(IRailway railway) =>
            Task.CompletedTask;
    }
}
