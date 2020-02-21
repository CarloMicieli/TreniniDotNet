using System.Threading.Tasks;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Common;

namespace TreniniDotNet.Domain.Catalog.Scales
{
    public sealed class ScaleService
    {
        private readonly IScalesFactory _scaleFactory;
        private readonly IScalesRepository _scaleRepository;

        public ScaleService(IScalesFactory scaleFactory, IScalesRepository scaleRepository)
        {
            _scaleFactory = scaleFactory;
            _scaleRepository = scaleRepository;
        }

        public async Task<Scale> CreateScale(IScale scale)
        {
            var s = (Scale)scale; //TODO
            await _scaleRepository.Add(s);
            return s;            
        }

        public async Task<IScale> CreateScale(string name, Ratio ratio, Gauge gauge, TrackGauge trackGauge, string? notes)
        {
            var scale = _scaleFactory.NewScale(name, ratio, gauge, trackGauge, notes);
            await _scaleRepository.Add(scale);
            return scale;
        }

        public async Task<bool> ScaleAlreadyExists(string name)
        {
            try
            {
                var slug = Slug.Of(name);
                var _ = await _scaleRepository.GetByAsync(slug);
                return true;
            }
            catch (ScaleNotFoundException)
            {
                return false;
            }
        }
    }
}
