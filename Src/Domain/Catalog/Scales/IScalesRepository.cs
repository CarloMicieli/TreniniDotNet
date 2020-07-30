using System.Threading.Tasks;
using TreniniDotNet.Common.Data;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Domain.Catalog.Scales
{
    public interface IScalesRepository : IRepository<ScaleId, Scale>
    {
        Task<bool> ExistsAsync(Slug slug);

        Task<Scale?> GetBySlugAsync(Slug slug);
    }
}
