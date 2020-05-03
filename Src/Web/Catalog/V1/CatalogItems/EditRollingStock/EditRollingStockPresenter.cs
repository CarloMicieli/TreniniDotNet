using TreniniDotNet.Application.Catalog.CatalogItems.EditRollingStock;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Web.Infrastructure.ViewModels;

namespace TreniniDotNet.Web.Catalog.V1.CatalogItems.EditRollingStock
{
    public sealed class EditRollingStockPresenter : DefaultHttpResultPresenter<EditRollingStockOutput>, IEditRollingStockOutputPort
    {
        public override void Standard(EditRollingStockOutput output)
        {
            throw new System.NotImplementedException();
        }

        public void RailwayWasNotFound(Slug slug)
        {
            throw new System.NotImplementedException();
        }

        public void RollingStockWasNotFound(Slug slug, RollingStockId rollingStockId)
        {
            throw new System.NotImplementedException();
        }

        public void CatalogItemWasNotFound(Slug slug)
        {
            throw new System.NotImplementedException();
        }
    }
}
