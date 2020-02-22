using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Common;
using System;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace TreniniDotNet.Infrastructure.Persistence.Catalog.Railways
{
    public sealed class RailwaysRepository : IRailwaysRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public RailwaysRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GuidId> Add(IRailway railway)
        {
            if (railway is null)
            {
                throw new ArgumentNullException(nameof(railway));
            }
            
            await _context.Railways.AddAsync(_mapper.Map<Railway>(railway));
            await _context.SaveChangesAsync();
            return railway.RailwayId;
        }

        public async Task<IRailway> GetBy(Slug slug)
        {
            var railway = await _context.Railways
                .Where(c => c.Slug == slug.ToString())
                .ProjectTo<Domain.Catalog.Railways.Railway>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();

            if (railway is null)
            {
                throw new RailwayNotFoundException(slug);
            }

            return railway;
        }
    }
}
