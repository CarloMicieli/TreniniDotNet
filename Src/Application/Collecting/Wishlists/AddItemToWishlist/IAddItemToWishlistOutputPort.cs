using TreniniDotNet.Common.UseCases.Boundaries.Outputs.Ports;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.Wishlists;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Application.Collecting.Wishlists.AddItemToWishlist
{
    public interface IAddItemToWishlistOutputPort : IStandardOutputPort<AddItemToWishlistOutput>
    {
        void WishlistNotFound(WishlistId wishlistId);

        void CatalogItemNotFound(Slug catalogItem);

        void CatalogItemAlreadyPresent(WishlistId wishlistId, CatalogItemRef catalogItem);

        void NotAuthorizedToEditThisWishlist(Owner owner);
    }
}
