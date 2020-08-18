using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Collecting.Wishlists.AddItemToWishlist;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.Wishlists;
using TreniniDotNet.SharedKernel.Slugs;
using TreniniDotNet.Web.Infrastructure.ViewModels;

namespace TreniniDotNet.Web.Collecting.V1.Wishlists.AddItemToWishlist
{
    public sealed class AddItemToWishlistPresenter : DefaultHttpResultPresenter<AddItemToWishlistOutput>, IAddItemToWishlistOutputPort
    {
        public void CatalogItemAlreadyPresent(WishlistId wishlistId, WishlistItemId itemId, CatalogItemRef catalogRef)
        {
            ViewModel = new ConflictObjectResult(new
            {
                Id = wishlistId,
                ItemId = itemId,
                CatalogItem = catalogRef.Slug
            });
        }

        public void CatalogItemNotFound(Slug catalogItem)
        {
            ViewModel = new NotFoundObjectResult(new
            {
                CatalogItem = catalogItem.Value
            });
        }

        public void CatalogItemAlreadyPresent(WishlistId wishlistId, CatalogItemRef catalogItem)
        {
            ViewModel = new ConflictObjectResult(new
            {
                Id = wishlistId,
                CatalogItem = catalogItem.Slug
            });
        }

        public void NotAuthorizedToEditThisWishlist(Owner owner)
        {
            ViewModel = new NotFoundObjectResult(new
            {
            });
        }

        public override void Standard(AddItemToWishlistOutput output)
        {
            ViewModel = new OkResult();
        }

        public void WishlistNotFound(WishlistId wishlistId)
        {
            ViewModel = new NotFoundObjectResult(new
            {
                Id = wishlistId
            });
        }
    }
}
