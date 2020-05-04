using TreniniDotNet.Common;
using TreniniDotNet.Common.UseCases.Interfaces.Output;

namespace TreniniDotNet.Application.Catalog.CatalogItems.AddRollingStockToCatalogItem
{
    public sealed class AddRollingStockToCatalogItemOutput : IUseCaseOutput
    {
        public AddRollingStockToCatalogItemOutput(Slug slug)
        {
            Slug = slug;
        }

        public Slug Slug { get; }
    }
}
