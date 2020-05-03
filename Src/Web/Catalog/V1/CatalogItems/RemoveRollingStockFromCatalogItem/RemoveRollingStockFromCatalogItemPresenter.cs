using TreniniDotNet.Application.Catalog.CatalogItems.RemoveRollingStockFromCatalogItem;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Web.Infrastructure.ViewModels;

namespace TreniniDotNet.Web.Catalog.V1.CatalogItems.RemoveRollingStockFromCatalogItem
{
    public sealed class RemoveRollingStockFromCatalogItemPresenter : DefaultHttpResultPresenter<RemoveRollingStockFromCatalogItemOutput>, IRemoveRollingStockFromCatalogItemOutputPort
    {
        public override void Standard(RemoveRollingStockFromCatalogItemOutput output)
        {
            throw new System.NotImplementedException();
        }

        public void RollingStockWasNotFound(Slug slug, RollingStockId rollingStockId)
        {
            throw new System.NotImplementedException();
        }
    }
}
