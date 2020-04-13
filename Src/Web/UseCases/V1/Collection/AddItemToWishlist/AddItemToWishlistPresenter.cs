using TreniniDotNet.Application.Boundaries.Collection.AddItemToWishlist;
using TreniniDotNet.Web.ViewModels;

namespace TreniniDotNet.Web.UseCases.V1.Collection.AddItemToWishlist
{
    public sealed class AddItemToWishlistPresenter : DefaultHttpResultPresenter<AddItemToWishlistOutput>, IAddItemToWishlistOutputPort
    {
        public override void Standard(AddItemToWishlistOutput output)
        {
            throw new System.NotImplementedException();
        }
    }
}
