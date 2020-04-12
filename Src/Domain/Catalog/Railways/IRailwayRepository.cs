using System.Threading.Tasks;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Domain.Pagination;

namespace TreniniDotNet.Domain.Catalog.Railways
{
    public interface IRailwaysRepository
    {
        Task<RailwayId> AddAsync(IRailway railway);

        Task<IRailway?> GetBySlugAsync(Slug slug);

        Task<bool> ExistsAsync(Slug slug);

        Task<PaginatedResult<IRailway>> GetRailwaysAsync(Page page);
    }
}
