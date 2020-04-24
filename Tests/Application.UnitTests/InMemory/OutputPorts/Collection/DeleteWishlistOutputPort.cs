using TreniniDotNet.Application.Boundaries.Collection.DeleteWishlist;
using TreniniDotNet.Domain.Collection.ValueObjects;

namespace TreniniDotNet.Application.InMemory.OutputPorts.Collection
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
