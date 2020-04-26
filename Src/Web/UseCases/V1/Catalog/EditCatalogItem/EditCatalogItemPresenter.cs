using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TreniniDotNet.Application.Boundaries.Catalog.EditCatalogItem;
using TreniniDotNet.Common;
using TreniniDotNet.Web.ViewModels;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.EditCatalogItem
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
