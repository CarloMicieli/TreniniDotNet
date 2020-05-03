using TreniniDotNet.Application.Catalog.CatalogItems.AddRollingStockToCatalogItem;
using TreniniDotNet.Common;
using TreniniDotNet.Web.Infrastructure.ViewModels;

namespace TreniniDotNet.Web.Catalog.V1.CatalogItems.AddRollingStockToCatalogItem
{
    public sealed class AddRollingStockToCatalogItemPresenter : DefaultHttpResultPresenter<AddRollingStockToCatalogItemOutput>, IAddRollingStockToCatalogItemOutputPort
    {
        public override void Standard(AddRollingStockToCatalogItemOutput output)
        {
            throw new System.NotImplementedException();
        }

        public void CatalogItemWasNotFound(Slug itemSlug)
        {
            throw new System.NotImplementedException();
        }

        public void RailwayWasNotFound(Slug railwaySlug)
        {
            throw new System.NotImplementedException();
        }
    }
}
