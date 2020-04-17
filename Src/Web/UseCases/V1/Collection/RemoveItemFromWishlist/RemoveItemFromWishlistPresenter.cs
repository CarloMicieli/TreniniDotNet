using TreniniDotNet.Application.Boundaries.Collection.RemoveItemFromWishlist;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Collection.Shared;
using TreniniDotNet.Domain.Collection.ValueObjects;
using TreniniDotNet.Web.ViewModels;

namespace TreniniDotNet.Web.UseCases.V1.Collection.RemoveItemFromWishlist
{
    public sealed class RemoveItemFromWishlistPresenter : DefaultHttpResultPresenter<RemoveItemFromWishlistOutput>, IRemoveItemFromWishlistOutputPort
    {
        public override void Standard(RemoveItemFromWishlistOutput output)
        {
            throw new System.NotImplementedException();
        }

        public void WishlistItemNotFound(Owner owner, Slug wishlistSlug, WishlistItemId itemId)
        {
            throw new System.NotImplementedException();
        }

        public void WishlistNotFound(Owner owner, Slug wishlistSlug)
        {
            throw new System.NotImplementedException();
        }
    }
}
