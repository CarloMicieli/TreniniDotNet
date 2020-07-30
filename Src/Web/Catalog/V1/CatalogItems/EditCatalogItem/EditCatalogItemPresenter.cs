using System.Collections.Generic;
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
            ViewModel = new ConflictObjectResult(new { Slug = brand });
        }

        public void CatalogItemNotFound(Slug slug)
        {
            ViewModel = new NotFoundObjectResult(new { Slug = slug });
        }

        public void RailwayNotFound(IEnumerable<Slug> railways)
        {
            throw new System.NotImplementedException();
        }

        public void ScaleNotFound(Slug scale)
        {
            ViewModel = new ConflictObjectResult(new { Slug = scale });
        }

        public override void Standard(EditCatalogItemOutput output)
        {
            ViewModel = new OkResult();
        }
    }
}
