using TreniniDotNet.Domain.Collection.ValueObjects;

namespace TreniniDotNet.Application.Boundaries.Collection.GetWishlistById
{
    public interface IGetWishlistByIdOutputPort : IOutputPortStandard<GetWishlistByIdOutput>
    {
        void WishlistNotFound(WishlistId id);
    }
}
