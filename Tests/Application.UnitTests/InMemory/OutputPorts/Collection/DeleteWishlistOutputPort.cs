using TreniniDotNet.Application.Boundaries.Collection.DeleteWishlist;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Collection.Shared;

namespace TreniniDotNet.Application.InMemory.OutputPorts.Collection
{
    public sealed class DeleteWishlistOutputPort : OutputPortTestHelper<DeleteWishlistOutput>, IDeleteWishlistOutputPort
    {
        private MethodInvocation<Owner, Slug> WishlistNotFoundMethod { set; get; }

        public DeleteWishlistOutputPort()
        {
            WishlistNotFoundMethod = MethodInvocation<Owner, Slug>.NotInvoked(nameof(WishlistNotFound));
        }

        public void WishlistNotFound(Owner owner, Slug wishlistSlug)
        {
            WishlistNotFoundMethod = WishlistNotFoundMethod.Invoked(owner, wishlistSlug);
        }

        public void AssertWishlistNotFound(Owner owner, Slug wishlistSlug) =>
            WishlistNotFoundMethod.ShouldBeInvokedWithTheArguments(owner, wishlistSlug);
    }
}
