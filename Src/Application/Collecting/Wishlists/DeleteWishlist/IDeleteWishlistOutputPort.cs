using TreniniDotNet.Common.UseCases.Interfaces.Output;
using TreniniDotNet.Domain.Collecting.ValueObjects;

namespace TreniniDotNet.Application.Collecting.Wishlists.DeleteWishlist
{
    public interface IDeleteWishlistOutputPort : IOutputPortStandard<DeleteWishlistOutput>
    {
        void WishlistNotFound(WishlistId id);
    }
}
