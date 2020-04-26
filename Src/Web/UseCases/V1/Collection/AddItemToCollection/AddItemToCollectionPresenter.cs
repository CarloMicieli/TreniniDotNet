using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Boundaries.Collection.AddItemToCollection;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Collection.Shared;
using TreniniDotNet.Web.ViewModels;

namespace TreniniDotNet.Web.UseCases.V1.Collection.AddItemToCollection
{
    public sealed class AddItemToCollectionPresenter : DefaultHttpResultPresenter<AddItemToCollectionOutput>, IAddItemToCollectionOutputPort
    {
        public void CatalogItemNotFound(Slug catalogItem)
        {
            ViewModel = new NotFoundObjectResult(new { CatalogItem = catalogItem.Value });
        }

        public void CollectionNotFound(Owner owner)
        {
            ViewModel = new NotFoundObjectResult(new { Owner = owner.Value });
        }

        public void ShopNotFound(string shop)
        {
            ViewModel = new NotFoundObjectResult(new { Shop = shop });
        }

        public override void Standard(AddItemToCollectionOutput output)
        {
            ViewModel = new OkObjectResult(new
            {
                Id = output.CollectionId.ToGuid(),
                ItemId = output.ItemId.ToGuid(),
                CatalogItem = output.CatalogItem.Value
            });
        }
    }
}
