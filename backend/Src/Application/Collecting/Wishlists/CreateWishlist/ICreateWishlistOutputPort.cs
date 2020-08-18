using TreniniDotNet.Common.UseCases.Boundaries.Outputs.Ports;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Application.Collecting.Wishlists.CreateWishlist
{
    public interface ICreateWishlistOutputPort : IStandardOutputPort<CreateWishlistOutput>
    {
        void WishlistAlreadyExists(Slug wishlistSlug);
    }
}
