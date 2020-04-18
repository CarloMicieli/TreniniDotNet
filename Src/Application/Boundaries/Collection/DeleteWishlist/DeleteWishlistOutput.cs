using TreniniDotNet.Domain.Collection.ValueObjects;

namespace TreniniDotNet.Application.Boundaries.Collection.DeleteWishlist
{
    public sealed class DeleteWishlistOutput : IUseCaseOutput
    {
        public DeleteWishlistOutput(WishlistId id)
        {
            Id = id;
        }

        public WishlistId Id { get; }
    }
}
