using TreniniDotNet.Common;
using TreniniDotNet.Domain.Collection.Shared;

namespace TreniniDotNet.Application.Boundaries.Collection.DeleteWishlist
{
    public interface IDeleteWishlistOutputPort : IOutputPortStandard<DeleteWishlistOutput>
    {
        void WishlistNotFound(Owner owner, Slug wishlistSlug);
    }
}
