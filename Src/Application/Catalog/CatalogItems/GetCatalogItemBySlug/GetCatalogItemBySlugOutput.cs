using TreniniDotNet.Common.UseCases.Interfaces.Output;
using TreniniDotNet.Domain.Catalog.CatalogItems;

namespace TreniniDotNet.Application.Catalog.CatalogItems.GetCatalogItemBySlug
{
    public sealed class GetCatalogItemBySlugOutput : IUseCaseOutput
    {
        private readonly ICatalogItem _item;

        public GetCatalogItemBySlugOutput(ICatalogItem item)
        {
            _item = item;
        }

        public ICatalogItem Item => _item;
    }
}
