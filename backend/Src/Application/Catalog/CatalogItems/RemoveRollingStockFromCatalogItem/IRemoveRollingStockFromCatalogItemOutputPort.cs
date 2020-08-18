using TreniniDotNet.Common.UseCases.Boundaries.Outputs.Ports;
using TreniniDotNet.Domain.Catalog.CatalogItems.RollingStocks;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Application.Catalog.CatalogItems.RemoveRollingStockFromCatalogItem
{
    public interface IRemoveRollingStockFromCatalogItemOutputPort : IStandardOutputPort<RemoveRollingStockFromCatalogItemOutput>
    {
        void RollingStockWasNotFound(Slug slug, RollingStockId rollingStockId);
    }
}
