using TreniniDotNet.Domain.Collecting.Wishlists;
using TreniniDotNet.TestHelpers.InMemory.OutputPorts;

namespace TreniniDotNet.Application.Collecting.Wishlists.DeleteWishlist
{
    public sealed class DeleteWishlistOutputPort : OutputPortTestHelper<DeleteWishlistOutput>, IDeleteWishlistOutputPort
    {
        private MethodInvocation<WishlistId> WishlistNotFoundMethod { set; get; }

        public DeleteWishlistOutputPort()
        {
            WishlistNotFoundMethod = MethodInvocation<WishlistId>.NotInvoked(nameof(WishlistNotFound));
        }

        public void WishlistNotFound(WishlistId id)
        {
            WishlistNotFoundMethod = WishlistNotFoundMethod.Invoked(id);
        }

        public void AssertWishlistNotFound(WishlistId expectedId) =>
            WishlistNotFoundMethod.ShouldBeInvokedWithTheArgument(expectedId);
    }
}
