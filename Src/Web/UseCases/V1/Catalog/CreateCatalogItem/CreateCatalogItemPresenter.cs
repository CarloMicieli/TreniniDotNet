using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TreniniDotNet.Application.Boundaries.Catalog.CreateCatalogItem;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Web.ViewModels;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.CreateCatalogItem
{
    public sealed class CreateCatalogItemPresenter : DefaultHttpResultPresenter<CreateCatalogItemOutput>, ICreateCatalogItemOutputPort
    {
        public void BrandNotFound(Slug brand)
        {
            ViewModel = new UnprocessableEntityObjectResult(brand.Value);
        }

        public void CatalogItemAlreadyExists(IBrandInfo brand, ItemNumber itemNumber)
        {
            ViewModel = new UnprocessableEntityObjectResult("");
        }

        public void RailwayNotFound(IEnumerable<Slug> railways)
        {
            ViewModel = new UnprocessableEntityObjectResult("");
        }

        public void ScaleNotFound(Slug scale)
        {
            ViewModel = new UnprocessableEntityObjectResult(scale.ToString());
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