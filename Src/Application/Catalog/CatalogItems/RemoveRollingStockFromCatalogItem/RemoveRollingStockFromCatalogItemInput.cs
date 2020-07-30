using TreniniDotNet.Common.UseCases.Boundaries.Inputs;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.CatalogItems.RollingStocks;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Application.Catalog.CatalogItems.RemoveRollingStockFromCatalogItem
{
    public sealed class RemoveRollingStockFromCatalogItemInput : IUseCaseInput
    {
        public RemoveRollingStockFromCatalogItemInput(Slug slug, RollingStockId rollingStockId)
        {
            CatalogItemSlug = slug;
            RollingStockId = rollingStockId;
        }

        public Slug CatalogItemSlug { get; }
        public RollingStockId RollingStockId { get; }
    }
}
