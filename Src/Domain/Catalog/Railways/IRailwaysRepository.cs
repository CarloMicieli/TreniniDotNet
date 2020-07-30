using System.Threading.Tasks;
using TreniniDotNet.Common.Data;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Domain.Catalog.Railways
{
    public interface IRailwaysRepository : IRepository<RailwayId, Railway>
    {
        Task<bool> ExistsAsync(Slug slug);

        Task<Railway?> GetBySlugAsync(Slug slug);
    }
}
