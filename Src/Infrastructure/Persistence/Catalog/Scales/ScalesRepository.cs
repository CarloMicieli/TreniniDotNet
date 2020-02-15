using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Common;
using System;

namespace TreniniDotNet.Infrastructure.Persistence.Catalog.Scales
{
    public sealed class ScalesRepository : IScalesRepository
    {
        private readonly ApplicationDbContext _context;

        public ScalesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ScaleId> Add(IScale scale)
        {
            if (scale is null)
            {
                throw new ArgumentNullException(nameof(scale));
            }

            await _context.Scales.AddAsync((Scale)scale);
            await _context.SaveChangesAsync();
            return scale.ScaleId;
        }

        public async Task<IScale> GetBy(Slug slug)
        {
            var scale = await _context.Scales
                .Where(s => s.Slug == slug)
                .SingleOrDefaultAsync();

            if (scale == null)
            {
                throw new ScaleNotFoundException(slug);
            }

            return scale;
        }
    }
}
