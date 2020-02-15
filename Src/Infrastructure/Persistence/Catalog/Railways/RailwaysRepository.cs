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

        public RailwaysRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<RailwayId> Add(IRailway railway)
        {
            if (railway is null)
            {
                throw new ArgumentNullException(nameof(railway));
            }
            
            await _context.Railways.AddAsync((Railway)railway);
            await _context.SaveChangesAsync();
            return railway.RailwayId;
        }

        public async Task<IRailway> GetBy(Slug slug)
        {
            var railway = await _context.Railways
                .Where(c => c.Slug == slug)
                .SingleOrDefaultAsync();

            if (railway is null)
            {
                throw new RailwayNotFoundException(slug);
            }

            return railway;
        }
    }
}
