using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Collecting.Collections.AddItemToCollection;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Web.Infrastructure.ViewModels;

namespace TreniniDotNet.Web.Collecting.V1.Collections.AddItemToCollection
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
