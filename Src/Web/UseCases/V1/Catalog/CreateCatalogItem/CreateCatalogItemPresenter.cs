using System.Collections.Generic;
using TreniniDotNet.Application.Boundaries.Catalog.CreateCatalogItem;
using TreniniDotNet.Web.ViewModels;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.CreateCatalogItem
{
    public sealed class CreateCatalogItemPresenter : DefaultHttpResultPresenter<CreateCatalogItemOutput>, ICreateCatalogItemOutputPort
    {
        public void BrandNameNotFound(string message)
        {
            throw new System.NotImplementedException();
        }

        public void CatalogItemAlreadyExists(string message)
        {
            throw new System.NotImplementedException();
        }

        public void RailwayNotFound(string message, IEnumerable<string> railwayNames)
        {
            throw new System.NotImplementedException();
        }

        public void ScaleNotFound(string message)
        {
            throw new System.NotImplementedException();
        }

        public override void Standard(CreateCatalogItemOutput output)
        {
            ViewModel = Created(
                nameof(GetCatalogItemBySlug.CatalogItemsController.GetCatalogItemBySlug),
                new
                {
                    slug = output.Slug,
                    version = "1",
                },
                output);
        }
    }
}