using TreniniDotNet.Common.UseCases.Boundaries.Outputs;
using TreniniDotNet.SharedKernel.Slugs;

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
