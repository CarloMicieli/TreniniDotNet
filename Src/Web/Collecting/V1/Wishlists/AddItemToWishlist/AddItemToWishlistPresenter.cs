using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Collecting.Wishlists.AddItemToWishlist;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.ValueObjects;
using TreniniDotNet.Web.Infrastructure.ViewModels;

namespace TreniniDotNet.Web.Collecting.V1.Wishlists.AddItemToWishlist
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
