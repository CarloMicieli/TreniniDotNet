using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Catalog.CatalogItems.CreateCatalogItem;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Web.Infrastructure.ViewModels;

namespace TreniniDotNet.Web.Catalog.V1.CatalogItems.CreateCatalogItem
{
    public sealed class CreateCatalogItemPresenter : DefaultHttpResultPresenter<CreateCatalogItemOutput>, ICreateCatalogItemOutputPort
    {
        public void BrandNotFound(Slug brand)
        {
            ViewModel = new UnprocessableEntityObjectResult($"Brand {brand.Value} was not found");
        }

        public void CatalogItemAlreadyExists(IBrandInfo brand, ItemNumber itemNumber)
        {
            ViewModel = new UnprocessableEntityObjectResult($"Catalog item {brand.Name} {itemNumber.Value} already exists");
        }

        public void RailwayNotFound(IEnumerable<Slug> railways)
        {
            ViewModel = new UnprocessableEntityObjectResult($"Railway not found {string.Join(", ", railways.Select(it => it.Value))}");
        }

        public void ScaleNotFound(Slug scale)
        {
            ViewModel = new UnprocessableEntityObjectResult($"{scale.Value} was not found");
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
