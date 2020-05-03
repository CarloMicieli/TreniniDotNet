using TreniniDotNet.Common;
using TreniniDotNet.Common.UseCases.Interfaces.Output;

namespace TreniniDotNet.Application.Catalog.CatalogItems.AddRollingStockToCatalogItem
{
    public interface IAddRollingStockToCatalogItemOutputPort : IOutputPortStandard<AddRollingStockToCatalogItemOutput>
    {
        void CatalogItemWasNotFound(Slug itemSlug);

        void RailwayWasNotFound(Slug railwaySlug);
    }
}
