using TreniniDotNet.Application.Boundaries.Collection.DeleteWishlist;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Collection.Shared;
using TreniniDotNet.Web.ViewModels;

namespace TreniniDotNet.Web.UseCases.V1.Collection.DeleteWishlist
{
    public sealed class DeleteWishlistPresenter : DefaultHttpResultPresenter<DeleteWishlistOutput>, IDeleteWishlistOutputPort
    {
        public override void Standard(DeleteWishlistOutput output)
        {
            throw new System.NotImplementedException();
        }

        public void WishlistNotFound(Owner owner, Slug wishlistSlug)
        {
            throw new System.NotImplementedException();
        }
    }
}
