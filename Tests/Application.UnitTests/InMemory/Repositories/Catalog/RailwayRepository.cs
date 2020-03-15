using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Domain.Pagination;

namespace TreniniDotNet.Application.InMemory.Repositories.Catalog
{
    public sealed class RailwayRepository : IRailwaysRepository
    {
        private readonly InMemoryContext _context;
        private readonly IRailwaysFactory _railwaysFactory;

        public RailwayRepository(InMemoryContext context)
        {
            _context = context;
            _railwaysFactory = new RailwaysFactory();
        }

        public Task<RailwayId> Add(
            string name,
            Slug slug,
            string companyName,
            string country,
            DateTime? operatingSince,
            DateTime? operatingUntil,
            RailwayStatus rs)
        {
            var newRailway = _railwaysFactory.NewRailway(
                name,
                companyName,
                country,
                operatingSince,
                operatingUntil,
                rs);

            _context.Railways.Add(newRailway);

            return Task.FromResult(newRailway.RailwayId);
        }

        public Task<bool> Exists(Slug slug)
        {
            var found = _context.Railways.Any(e => e.Slug == slug);
            return Task.FromResult(found);
        }

        public Task<List<IRailway>> GetAll()
        {
            return Task.FromResult(_context.Railways.ToList());
        }

        public Task<IRailway> GetBy(Slug slug)
        {
            IRailway railway = _context.Railways.FirstOrDefault(e => e.Slug == slug);
            return Task.FromResult(railway);
        }

        public Task<IRailway> GetByName(string name)
        {
            IRailway railway = _context.Railways
                .FirstOrDefault(e => e.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
            return Task.FromResult(railway);
        }

        public Task<PaginatedResult<IRailway>> GetRailways(Page page)
        {
            var results = _context.Railways
                .OrderBy(r => r.Name)
                .Skip(page.Start)
                .Take(page.Limit + 1)
                .ToList();

            return Task.FromResult(new PaginatedResult<IRailway>(page, results));
        }
    }
}
