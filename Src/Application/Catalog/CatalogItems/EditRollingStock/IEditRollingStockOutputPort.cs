using TreniniDotNet.Common;
using TreniniDotNet.Common.UseCases.Interfaces.Output;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Application.Catalog.CatalogItems.EditRollingStock
{
    public interface IEditRollingStockOutputPort : IOutputPortStandard<EditRollingStockOutput>
    {
        void RailwayWasNotFound(Slug slug);

        void RollingStockWasNotFound(Slug slug, RollingStockId rollingStockId);

        void CatalogItemWasNotFound(Slug slug);
    }
}
