using TreniniDotNet.Application.Boundaries.Collection.GetWishlistById;
using TreniniDotNet.Domain.Collection.ValueObjects;

namespace TreniniDotNet.Application.InMemory.OutputPorts.Collection
{
    public sealed class GetWishlistByIdOutputPort : OutputPortTestHelper<GetWishlistByIdOutput>, IGetWishlistByIdOutputPort
    {
        private MethodInvocation<WishlistId> WishlistNotFoundMethod { set; get; }

        public GetWishlistByIdOutputPort()
        {
            WishlistNotFoundMethod = MethodInvocation<WishlistId>.NotInvoked(nameof(WishlistNotFound));
        }

        public void WishlistNotFound(WishlistId id)
        {
            WishlistNotFoundMethod = WishlistNotFoundMethod.Invoked(id);
        }

        public void AssertWishlistWasNotFound(WishlistId expectedId) =>
            WishlistNotFoundMethod.ShouldBeInvokedWithTheArgument(expectedId);
    }
}
