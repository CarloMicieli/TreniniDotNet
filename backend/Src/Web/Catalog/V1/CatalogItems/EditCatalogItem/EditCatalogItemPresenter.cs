using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Catalog.CatalogItems.EditCatalogItem;
using TreniniDotNet.SharedKernel.Slugs;
using TreniniDotNet.Web.Infrastructure.ViewModels;

namespace TreniniDotNet.Web.Catalog.V1.CatalogItems.EditCatalogItem
{
    public sealed class EditCatalogItemPresenter : DefaultHttpResultPresenter<EditCatalogItemOutput>, IEditCatalogItemOutputPort
    {
        public void BrandNotFound(Slug brand)
        {
            ViewModel = new ConflictObjectResult(new { brand = brand });
        }

        public void CatalogItemNotFound(Slug slug)
        {
            ViewModel = new NotFoundObjectResult(new { catalogItem = slug });
        }

        public void RailwayNotFound(IEnumerable<Slug> railways)
        {
            ViewModel = new ConflictObjectResult(new { railways = railways.Select(it => it.Value) });
        }

        public void ScaleNotFound(Slug scale)
        {
            ViewModel = new ConflictObjectResult(new { scale });
        }

        public override void Standard(EditCatalogItemOutput output)
        {
            ViewModel = new OkResult();
        }
    }
}
