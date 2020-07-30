using TreniniDotNet.Common.UseCases.Boundaries.Outputs.Ports;
using TreniniDotNet.Domain.Collecting.Wishlists;

namespace TreniniDotNet.Application.Collecting.Wishlists.DeleteWishlist
{
    public interface IDeleteWishlistOutputPort : IStandardOutputPort<DeleteWishlistOutput>
    {
        void WishlistNotFound(WishlistId id);
    }
}
