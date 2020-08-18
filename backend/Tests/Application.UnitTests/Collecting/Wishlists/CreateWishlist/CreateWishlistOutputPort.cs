using TreniniDotNet.SharedKernel.Slugs;
using TreniniDotNet.TestHelpers.InMemory.OutputPorts;

namespace TreniniDotNet.Application.Collecting.Wishlists.CreateWishlist
{
    public sealed class CreateWishlistOutputPort : OutputPortTestHelper<CreateWishlistOutput>, ICreateWishlistOutputPort
    {
        private MethodInvocation<Slug> WishlistAlreadyExistsMethod { set; get; }

        public CreateWishlistOutputPort()
        {
            WishlistAlreadyExistsMethod = MethodInvocation<Slug>.NotInvoked(nameof(WishlistAlreadyExists));
        }

        public void WishlistAlreadyExists(Slug wishlistSlug)
        {
            WishlistAlreadyExistsMethod = WishlistAlreadyExistsMethod.Invoked(wishlistSlug);
        }

        public void AssertWishlistAlreadyExistsWithSlug(Slug expectedSlug) =>
            WishlistAlreadyExistsMethod.ShouldBeInvokedWithTheArgument(expectedSlug);

    }
}
