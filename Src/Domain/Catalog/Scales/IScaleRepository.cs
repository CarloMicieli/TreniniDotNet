using System.Threading.Tasks;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Pagination;

namespace TreniniDotNet.Domain.Catalog.Scales
{
    public interface IScalesRepository
    {
        Task<ScaleId> Add(IScale scale);
        Task<IScale?> GetBySlugAsync(Slug slug);
        Task<bool> ExistsAsync(Slug slug);
        Task<PaginatedResult<IScale>> GetScalesAsync(Page page);
    }
}
