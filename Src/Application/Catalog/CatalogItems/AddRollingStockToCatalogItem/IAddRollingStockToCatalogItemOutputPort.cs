using TreniniDotNet.Common.UseCases.Boundaries.Outputs.Ports;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Application.Catalog.CatalogItems.AddRollingStockToCatalogItem
{
    public interface IAddRollingStockToCatalogItemOutputPort : IStandardOutputPort<AddRollingStockToCatalogItemOutput>
    {
        void CatalogItemWasNotFound(Slug itemSlug);

        void RailwayWasNotFound(Slug railwaySlug);
    }
}
