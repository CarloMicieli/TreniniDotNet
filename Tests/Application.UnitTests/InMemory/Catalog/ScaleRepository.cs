using System.Linq;
using System.Threading.Tasks;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Application.InMemory.Catalog
{
    public sealed class ScaleRepository : IScalesRepository
    {
        private readonly InMemoryContext _context;

        public ScaleRepository(InMemoryContext context)
        {
            _context = context;
        }
        
        public Task<ScaleId> Add(IScale scale)
        {
            _context.Scales.Add((Scale) scale);
            return Task.FromResult(ScaleId.Empty);
        }

        public Task<ScaleId> Create(Scale scale)
        {
            throw new System.NotImplementedException();
        }

        public Task<IScale> GetBy(Slug slug)
        {
            IScale scale = _context.Scales.FirstOrDefault(e => e.Slug == slug);
            if (scale == null)
            {
                throw new ScaleNotFoundException(slug);
            }
            return Task.FromResult(scale);
        }

        public Task<Scale> GetByAsync(Slug slug)
        {
            IScale scale = _context.Scales.FirstOrDefault(e => e.Slug == slug);
            if (scale == null)
            {
                throw new ScaleNotFoundException(slug);
            }
            return Task.FromResult((Scale) scale);
        }
    }
}
