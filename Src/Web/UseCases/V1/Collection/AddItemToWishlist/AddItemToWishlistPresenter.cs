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
            throw new System.NotImplementedException();
        }

        public void CatalogItemNotFound(Slug catalogItem)
        {
            throw new System.NotImplementedException();
        }

        public override void Standard(AddItemToWishlistOutput output)
        {
            throw new System.NotImplementedException();
        }

        public void WishlistNotFound(WishlistId wishlistId)
        {
            throw new System.NotImplementedException();
        }
    }
}
