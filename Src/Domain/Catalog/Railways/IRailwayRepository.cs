using System.Threading.Tasks;
using TreniniDotNet.Common;
using TreniniDotNet.Common.Pagination;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Domain.Catalog.Railways
{
    public interface IRailwaysRepository
    {
        Task<RailwayId> AddAsync(IRailway railway);

        Task<IRailway?> GetBySlugAsync(Slug slug);

        Task<IRailwayInfo?> GetInfoBySlugAsync(Slug slug);

        Task<bool> ExistsAsync(Slug slug);

        Task<PaginatedResult<IRailway>> GetRailwaysAsync(Page page);

        Task UpdateAsync(IRailway railway);
    }
}
