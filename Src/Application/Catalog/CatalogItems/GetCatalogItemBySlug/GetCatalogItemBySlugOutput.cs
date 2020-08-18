using TreniniDotNet.Common.UseCases.Boundaries.Outputs;
using TreniniDotNet.Domain.Catalog.CatalogItems;

namespace TreniniDotNet.Application.Catalog.CatalogItems.GetCatalogItemBySlug
{
    public sealed class GetCatalogItemBySlugOutput : IUseCaseOutput
    {
        public GetCatalogItemBySlugOutput(CatalogItem item)
        {
            Item = item;
        }

        public CatalogItem Item { get; }
    }
}
