using TreniniDotNet.Common.UseCases.Boundaries.Outputs.Ports;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.CatalogItems.RollingStocks;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Application.Catalog.CatalogItems.EditRollingStock
{
    public interface IEditRollingStockOutputPort : IStandardOutputPort<EditRollingStockOutput>
    {
        void RailwayWasNotFound(Slug slug);

        void RollingStockWasNotFound(Slug slug, RollingStockId rollingStockId);

        void CatalogItemWasNotFound(Slug slug);
    }
}
