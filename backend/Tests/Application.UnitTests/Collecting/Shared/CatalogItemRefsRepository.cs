using System.Linq;
using System.Threading.Tasks;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.SharedKernel.Slugs;
using TreniniDotNet.TestHelpers.InMemory.Repository;

namespace TreniniDotNet.Application.Collecting.Shared
{
    public class CatalogItemRefsRepository : ICatalogItemRefsRepository
    {
        private InMemoryContext Context { get; }

        public CatalogItemRefsRepository(InMemoryContext context)
        {
            Context = context;
        }

        public Task<CatalogItemRef> GetCatalogItemAsync(Slug slug)
        {
            var result = Context.CatalogItems
                .FirstOrDefault(it => it.Slug == slug);
            if (result is null)
            {
                return Task.FromResult<CatalogItemRef>(null);
            }

            return Task.FromResult(new CatalogItemRef(result));
        }
    }
}