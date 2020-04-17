using TreniniDotNet.Common;

namespace TreniniDotNet.Application.Boundaries.Collection.CreateWishlist
{
    public interface ICreateWishlistOutputPort : IOutputPortStandard<CreateWishlistOutput>
    {
        void WishlistAlreadyExists(Slug wishlistSlug);
    }
}
