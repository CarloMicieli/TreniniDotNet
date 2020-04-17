using System.Threading.Tasks;
using TreniniDotNet.Common;

namespace TreniniDotNet.Domain.Collection.Shared
{
    public interface ICatalogRefsRepository
    {
        Task<ICatalogRef> GetBySlugAsync(Slug slug);
    }
}
