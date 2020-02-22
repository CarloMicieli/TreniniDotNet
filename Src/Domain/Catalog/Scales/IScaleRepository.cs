using System.Threading.Tasks;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Common;

namespace TreniniDotNet.Domain.Catalog.Scales
{
    public interface IScalesRepository
    {
        Task<ScaleId> Add(ScaleId scaleId, Slug slug, string name, Ratio ratio, Gauge gauge, TrackGauge trackGauge, string? notes);
        
        Task<IScale> GetBy(Slug slug);

        Task<bool> Exists(Slug slug);
    }
}
