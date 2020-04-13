using TreniniDotNet.Application.Boundaries.Collection.GetWishlistById;
using TreniniDotNet.Web.ViewModels;

namespace TreniniDotNet.Web.UseCases.V1.Collection.GetWishlistById
{
    public sealed class GetWishlistByIdPresenter : DefaultHttpResultPresenter<GetWishlistByIdOutput>, IGetWishlistByIdOutputPort
    {
        public override void Standard(GetWishlistByIdOutput output)
        {
            throw new System.NotImplementedException();
        }
    }
}
