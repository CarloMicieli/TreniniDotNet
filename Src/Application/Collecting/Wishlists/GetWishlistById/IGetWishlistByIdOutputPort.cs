using TreniniDotNet.Common.UseCases.Interfaces.Output;
using TreniniDotNet.Domain.Collecting.ValueObjects;

namespace TreniniDotNet.Application.Collecting.Wishlists.GetWishlistById
{
    public interface IGetWishlistByIdOutputPort : IOutputPortStandard<GetWishlistByIdOutput>
    {
        void WishlistNotFound(WishlistId id);

        void WishlistNotVisible(WishlistId id, Visibility visibility);
    }
}
