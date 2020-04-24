using TreniniDotNet.Domain.Collection.ValueObjects;

namespace TreniniDotNet.Application.Boundaries.Collection.DeleteWishlist
{
    public interface IDeleteWishlistOutputPort : IOutputPortStandard<DeleteWishlistOutput>
    {
        void WishlistNotFound(WishlistId id);
    }
}
