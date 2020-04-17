using TreniniDotNet.Application.Boundaries.Collection.AddItemToWishlist;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Collection.Shared;
using TreniniDotNet.Web.ViewModels;

namespace TreniniDotNet.Web.UseCases.V1.Collection.AddItemToWishlist
{
    public sealed class AddItemToWishlistPresenter : DefaultHttpResultPresenter<AddItemToWishlistOutput>, IAddItemToWishlistOutputPort
    {
        public void CatalogItemAlreadyPresent(Owner owner, Slug wishlistSlug, Slug catalogItem)
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

        public void WishlistNotFound(Owner owner, Slug wishlistSlug)
        {
            throw new System.NotImplementedException();
        }
    }
}
