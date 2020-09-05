using TreniniDotNet.Common.UseCases.Boundaries.Outputs.Ports;
using TreniniDotNet.Domain.Collecting.Wishlists;

namespace TreniniDotNet.Application.Collecting.Wishlists.GetWishlistById
{
    public interface IGetWishlistByIdOutputPort : IStandardOutputPort<GetWishlistByIdOutput>
    {
        void WishlistNotFound(WishlistId id);

        void WishlistNotVisible(WishlistId id, Visibility visibility);
    }
}
