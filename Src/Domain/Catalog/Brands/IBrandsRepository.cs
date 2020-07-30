using System.Threading.Tasks;
using TreniniDotNet.Common.Data;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Domain.Catalog.Brands
{
    public interface IBrandsRepository : IRepository<BrandId, Brand>
    {
        Task<bool> ExistsAsync(Slug slug);

        Task<Brand?> GetBySlugAsync(Slug slug);
    }
}