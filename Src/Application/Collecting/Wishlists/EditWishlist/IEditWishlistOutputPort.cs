using TreniniDotNet.Common.UseCases.Boundaries.Outputs.Ports;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.Wishlists;

namespace TreniniDotNet.Application.Collecting.Wishlists.EditWishlist
{
    public interface IEditWishlistOutputPort : IStandardOutputPort<EditWishlistOutput>
    {
        void WishlistNotFound(WishlistId id);

        void NotAuthorizedToEditThisWishlist(Owner owner);
    }
}
