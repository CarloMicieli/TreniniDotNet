using System.Threading.Tasks;
using TreniniDotNet.Common.Data;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Domain.Collecting.Shops
{
    public interface IShopsRepository : IRepository<ShopId, Shop>
    {
        Task<bool> ExistsAsync(ShopId shopId);

        Task<bool> ExistsAsync(Slug slug);

        Task<Shop?> GetBySlugAsync(Slug slug);
    }
}
