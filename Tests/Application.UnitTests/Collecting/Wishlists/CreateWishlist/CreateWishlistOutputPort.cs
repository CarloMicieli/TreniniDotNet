using TreniniDotNet.Application.Collecting.Wishlists.CreateWishlist;
using TreniniDotNet.Common;
using TreniniDotNet.TestHelpers.InMemory.OutputPorts;

namespace TreniniDotNet.Application.InMemory.Collecting.Wishlists.OutputPorts
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
