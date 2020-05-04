using TreniniDotNet.Common;
using TreniniDotNet.Common.UseCases.Interfaces.Output;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Application.Catalog.CatalogItems.RemoveRollingStockFromCatalogItem
{
    public interface IRemoveRollingStockFromCatalogItemOutputPort : IOutputPortStandard<RemoveRollingStockFromCatalogItemOutput>
    {
        void RollingStockWasNotFound(Slug slug, RollingStockId rollingStockId);
    }
}
