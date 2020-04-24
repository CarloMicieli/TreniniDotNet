using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Boundaries.Collection.DeleteWishlist;
using TreniniDotNet.Domain.Collection.ValueObjects;
using TreniniDotNet.Web.ViewModels;

namespace TreniniDotNet.Web.UseCases.V1.Collection.DeleteWishlist
{
    public sealed class DeleteWishlistPresenter : DefaultHttpResultPresenter<DeleteWishlistOutput>, IDeleteWishlistOutputPort
    {
        public override void Standard(DeleteWishlistOutput output)
        {
            ViewModel = new NoContentResult();
        }

        public void WishlistNotFound(WishlistId id)
        {
            ViewModel = new NotFoundObjectResult(new { Id = id.ToGuid() });
        }
    }
}
