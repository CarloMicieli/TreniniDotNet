using TreniniDotNet.Common.UseCases.Boundaries.Outputs;
using TreniniDotNet.Domain.Collecting.Wishlists;

namespace TreniniDotNet.Application.Collecting.Wishlists.DeleteWishlist
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
