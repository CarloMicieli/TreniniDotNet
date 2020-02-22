using System;
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
        private readonly IScalesFactory _scalesFactory;

        public ScaleRepository(InMemoryContext context)
        {
            _context = context ??
                throw new ArgumentNullException(nameof(context));

            _scalesFactory = new ScalesFactory();
        }

        public Task<ScaleId> Add(
            ScaleId scaleId, Slug slug, string name, Ratio ratio, Gauge gauge, TrackGauge trackGauge, string notes)
        {
            _context.Scales.Add(_scalesFactory.NewScale(
                scaleId.ToGuid(),
                name,
                slug.Value,
                ratio.ToDecimal(),
                gauge.ToDecimal(MeasureUnit.Millimeters),
                trackGauge.ToString(),
                notes));
            return Task.FromResult(scaleId);
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

        public Task<bool> Exists(Slug slug)
        {
            var found = _context.Scales.Any(e => e.Slug == slug);
            return Task.FromResult(found);
        }
    }
}
