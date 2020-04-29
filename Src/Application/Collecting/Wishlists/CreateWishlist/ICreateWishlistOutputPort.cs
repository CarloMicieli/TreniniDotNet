using TreniniDotNet.Common;
using TreniniDotNet.Common.UseCases.Interfaces.Output;

namespace TreniniDotNet.Application.Collecting.Wishlists.CreateWishlist
{
    public interface ICreateWishlistOutputPort : IOutputPortStandard<CreateWishlistOutput>
    {
        void WishlistAlreadyExists(Slug wishlistSlug);
    }
}
