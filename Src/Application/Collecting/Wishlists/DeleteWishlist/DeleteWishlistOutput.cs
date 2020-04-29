using TreniniDotNet.Common.UseCases.Interfaces.Output;
using TreniniDotNet.Domain.Collecting.ValueObjects;

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
