using System.Threading.Tasks;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Common;
using System.Collections.Generic;
using TreniniDotNet.Domain.Pagination;

namespace TreniniDotNet.Domain.Catalog.Scales
{
    public interface IScalesRepository
    {
        Task<ScaleId> Add(ScaleId scaleId, Slug slug, string name, Ratio ratio, Gauge gauge, TrackGauge trackGauge, string? notes);
        Task<IScale> GetBy(Slug slug);
        Task<bool> Exists(Slug slug);
        Task<List<IScale>> GetAll();
        Task<PaginatedResult<IScale>> GetScales(Page page);
        Task<IScale?> GetByName(string name);
    }
}
