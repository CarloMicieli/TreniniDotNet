using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Boundaries.Collection.AddItemToWishlist;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Collection.Shared;
using TreniniDotNet.Domain.Collection.ValueObjects;
using TreniniDotNet.Web.ViewModels;

namespace TreniniDotNet.Web.UseCases.V1.Collection.AddItemToWishlist
{
    public sealed class AddItemToWishlistPresenter : DefaultHttpResultPresenter<AddItemToWishlistOutput>, IAddItemToWishlistOutputPort
    {
        public void CatalogItemAlreadyPresent(WishlistId wishlistId, WishlistItemId itemId, ICatalogRef catalogRef)
        {
            ViewModel = new ConflictObjectResult(new
            {
                Id = wishlistId.ToGuid(),
                ItemId = itemId.ToGuid(),
                CatalogItem = catalogRef.Slug.Value
            });
        }

        public void CatalogItemNotFound(Slug catalogItem)
        {
            ViewModel = new NotFoundObjectResult(new
            {
                CatalogItem = catalogItem.Value
            });
        }

        public override void Standard(AddItemToWishlistOutput output)
        {
            ViewModel = new OkObjectResult(new
            {
                Id = output.Id.ToGuid(),
                ItemId = output.ItemId.ToGuid()
            });
        }

        public void WishlistNotFound(WishlistId wishlistId)
        {
            ViewModel = new NotFoundObjectResult(new
            {
                Id = wishlistId.ToGuid()
            });
        }
    }
}
