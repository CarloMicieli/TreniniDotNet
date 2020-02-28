using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Common;
using System;
using System.Collections.Generic;
using TreniniDotNet.Domain.Pagination;

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

            var railway = new Railway 
            {
                RailwayId = newId,
                Name = name,
                Slug = slug.ToString(),
                CompanyName = companyName,
                Country = country,
                Status = rs.ToString(),
                Version = 1,
                OperatingSince = operatingSince,
                OperatingUntil = operatingUntil
            };

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

        public Task<List<IRailway>> GetAll()
        {
            return _context.Railways
                .Select(r => _railwaysFactory.NewRailway(
                    new RailwayId(r.RailwayId),
                    r.Name,
                    Slug.Of(r.Slug),
                    r.CompanyName,
                    r.Country,
                    r.OperatingSince,
                    r.OperatingUntil,
                    r.Status.ToRailwayStatus()))
                .ToListAsync();
        }

        public async Task<PaginatedResult<IRailway>> GetRailways(Page page)
        {
            var results = await _context.Railways
                .OrderBy(r => r.Name)
                .Skip(page.Start)
                .Take(page.Limit + 1)
                .Select(r => _railwaysFactory.NewRailway(
                    new RailwayId(r.RailwayId),
                    r.Name,
                    Slug.Of(r.Slug),
                    r.CompanyName,
                    r.Country,
                    r.OperatingSince,
                    r.OperatingUntil,
                    r.Status.ToRailwayStatus()))
                .ToListAsync();

            return new PaginatedResult<IRailway>(page, results);
        }

        public Task<IRailway?> GetByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
