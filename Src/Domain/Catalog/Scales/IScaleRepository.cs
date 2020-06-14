using System.Threading.Tasks;
using TreniniDotNet.Common;
using TreniniDotNet.Common.Pagination;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Domain.Catalog.Scales
{
    public interface IScalesRepository
    {
        Task<ScaleId> AddAsync(IScale scale);

        Task<IScale?> GetBySlugAsync(Slug slug);

        Task<IScaleInfo?> GetInfoBySlugAsync(Slug slug);

        Task<bool> ExistsAsync(Slug slug);

        Task<PaginatedResult<IScale>> GetScalesAsync(Page page);

        Task UpdateAsync(IScale scale);
    }
}
