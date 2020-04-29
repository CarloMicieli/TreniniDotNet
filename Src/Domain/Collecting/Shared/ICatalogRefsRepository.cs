using System.Threading.Tasks;
using TreniniDotNet.Common;

namespace TreniniDotNet.Domain.Collecting.Shared
{
    public interface ICatalogRefsRepository
    {
        Task<ICatalogRef?> GetBySlugAsync(Slug slug);
    }
}
