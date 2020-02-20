using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Common;
using System;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace TreniniDotNet.Infrastructure.Persistence.Catalog.Scales
{
    public sealed class ScalesRepository : IScalesRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ScalesRepository(ApplicationDbContext context, IMapper mapper)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            
            if (mapper is null)
            {
                throw new ArgumentNullException(nameof(mapper));
            }

            _context = context;
            _mapper = mapper;
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

        public async Task<ScaleId> Create(Domain.Catalog.Scales.Scale scale)
        {
            var newScale = _mapper.Map<Scale>(scale);
            _context.Add(newScale);
            var _ = await _context.SaveChangesAsync();
            return newScale.ScaleId;            
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

        public async Task<Domain.Catalog.Scales.Scale> GetByAsync(Slug slug)
        {
            var scale = await _context.Scales
                .Where(s => s.Slug == slug)
                .ProjectTo<Domain.Catalog.Scales.Scale>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();

            if (scale is null)
            {
                throw new ScaleNotFoundException(slug);
            }

            return scale;                
        }
    }
}
