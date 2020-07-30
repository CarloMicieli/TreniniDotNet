using TreniniDotNet.Domain.Collecting.Wishlists;
using TreniniDotNet.TestHelpers.InMemory.OutputPorts;

namespace TreniniDotNet.Application.Collecting.Wishlists.GetWishlistById
{
    public sealed class GetWishlistByIdOutputPort : OutputPortTestHelper<GetWishlistByIdOutput>, IGetWishlistByIdOutputPort
    {
        private MethodInvocation<WishlistId> WishlistNotFoundMethod { set; get; }
        private MethodInvocation<WishlistId, Visibility> WishlistNotVisibleMethod { set; get; }

        public GetWishlistByIdOutputPort()
        {
            WishlistNotFoundMethod = MethodInvocation<WishlistId>.NotInvoked(nameof(WishlistNotFound));
            WishlistNotVisibleMethod = MethodInvocation<WishlistId, Visibility>.NotInvoked(nameof(WishlistNotVisible));
        }

        public void WishlistNotFound(WishlistId id)
        {
            WishlistNotFoundMethod = WishlistNotFoundMethod.Invoked(id);
        }

        public void WishlistNotVisible(WishlistId id, Visibility visibility)
        {
            throw new System.NotImplementedException();
        }

        public void AssertWishlistWasNotFound(WishlistId expectedId) =>
            WishlistNotFoundMethod.ShouldBeInvokedWithTheArgument(expectedId);

        public void AssertWishlistWasNotVisibile(WishlistId expectedId, Visibility expectedVisibility) =>
            WishlistNotVisibleMethod.ShouldBeInvokedWithTheArguments(expectedId, expectedVisibility);
    }
}
