using TreniniDotNet.Application.Boundaries.Collection.CreateWishlist;
using TreniniDotNet.Web.ViewModels;

namespace TreniniDotNet.Web.UseCases.V1.Collection.CreateWishlist
{
    public sealed class CreateWishlistPresenter : DefaultHttpResultPresenter<CreateWishlistOutput>, ICreateWishlistOutputPort
    {
        public override void Standard(CreateWishlistOutput output)
        {
            throw new System.NotImplementedException();
        }
    }
}
