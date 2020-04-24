using System.Linq;
using System.Threading.Tasks;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Collection.Shared;

namespace TreniniDotNet.Application.InMemory.Repositories.Collection
{
    public class CatalogRefsRepository : ICatalogRefsRepository
    {
        private readonly InMemoryContext _context;

        public CatalogRefsRepository(InMemoryContext context)
        {
            _context = context;
        }

        public Task<ICatalogRef> GetBySlugAsync(Slug slug)
        {
            var result = _context.CatalogItems.Where(it => it.Slug == slug)
                .Select(it => CatalogRef.Of(it.CatalogItemId, it.Slug))
                .FirstOrDefault();
            return Task.FromResult(result);
        }
    }
}
