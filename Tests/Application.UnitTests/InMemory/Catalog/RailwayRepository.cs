using System;
using System.Linq;
using System.Threading.Tasks;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Application.InMemory.Catalog
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

        public Task<IRailway> GetBy(Slug slug)
        {
            IRailway railway = _context.Railways.FirstOrDefault(e => e.Slug == slug);
            return Task.FromResult(railway);
        }
    }
}
