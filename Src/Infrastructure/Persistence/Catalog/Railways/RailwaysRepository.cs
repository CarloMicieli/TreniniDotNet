using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Common;
using System;

namespace TreniniDotNet.Infrastructure.Persistence.Catalog.Railways
{
    public sealed class RailwaysRepository : IRailwaysRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IRailwaysFactory _railwaysFactory;

        public RailwaysRepository(ApplicationDbContext context, IRailwaysFactory railwaysFactory)
        {
            _context = context;
            _railwaysFactory = railwaysFactory;
        }

        public Task<RailwayId> Add(string name, Slug slug, string? companyName, string? country, DateTime? operatingSince, DateTime? operatingUntil, RailwayStatus rs)
        {
            var newId = Guid.NewGuid();

            var railway = new Railway { };

            _context.Add(railway);
            return Task.FromResult(new RailwayId(newId));
        }

        public Task<bool> Exists(Slug slug)
        {
            return _context.Railways.AnyAsync(r => r.Slug == slug.ToString());
        }

        public Task<IRailway> GetBy(Slug slug)
        {
            return _context.Railways
                .Where(r => r.Slug == slug.ToString())
                .Select(r => _railwaysFactory.NewRailway(
                    new RailwayId(r.RailwayId),
                    r.Name,
                    Slug.Of(r.Slug),
                    r.CompanyName,
                    r.Country,
                    r.OperatingSince,
                    r.OperatingUntil,
                    r.Status.ToRailwayStatus()))
                .SingleOrDefaultAsync();
        }
    }
}
