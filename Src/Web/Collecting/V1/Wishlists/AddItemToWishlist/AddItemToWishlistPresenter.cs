using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Collecting.Wishlists.AddItemToWishlist;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.Wishlists;
using TreniniDotNet.SharedKernel.Slugs;
using TreniniDotNet.Web.Infrastructure.ViewModels;

namespace TreniniDotNet.Web.Collecting.V1.Wishlists.AddItemToWishlist
{
    public sealed class AddItemToWishlistPresenter : DefaultHttpResultPresenter<AddItemToWishlistOutput>, IAddItemToWishlistOutputPort
    {
        public void CatalogItemAlreadyPresent(WishlistId wishlistId, WishlistItemId itemId, CatalogItem catalogRef)
        {
            ViewModel = new ConflictObjectResult(new
            {
                Id = wishlistId,
                ItemId = itemId,
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

        public void CatalogItemAlreadyPresent(WishlistId wishlistId, CatalogItem catalogItem)
        {
            throw new System.NotImplementedException();
        }

        public void NotAuthorizedToEditThisWishlist(Owner owner)
        {
            throw new System.NotImplementedException();
        }

        public override void Standard(AddItemToWishlistOutput output)
        {
            ViewModel = new OkObjectResult(new
            {
                Id = output.Id,
                ItemId = output.ItemId
            });
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
