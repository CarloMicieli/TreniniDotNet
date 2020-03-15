using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TreniniDotNet.Application.Boundaries.Catalog.CreateCatalogItem;
using TreniniDotNet.Web.ViewModels;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.CreateCatalogItem
{
    public sealed class CreateCatalogItemPresenter : DefaultHttpResultPresenter<CreateCatalogItemOutput>, ICreateCatalogItemOutputPort
    {
        public void BrandNameNotFound(string message)
        {
            ViewModel = new UnprocessableEntityObjectResult(message);
        }

        public void CatalogItemAlreadyExists(string message)
        {
            ViewModel = new UnprocessableEntityObjectResult(message);
        }

        public void RailwayNotFound(string message, IEnumerable<string> railwayNames)
        {
            ViewModel = new UnprocessableEntityObjectResult(message);
        }

        public void ScaleNotFound(string message)
        {
            ViewModel = new UnprocessableEntityObjectResult(message);
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