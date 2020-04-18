using TreniniDotNet.Domain.Collection.Shared;
using TreniniDotNet.Domain.Collection.ValueObjects;

namespace TreniniDotNet.Application.Boundaries.Collection.GetWishlistsByOwner
{
    public interface IGetWishlistsByOwnerOutputPort : IOutputPortStandard<GetWishlistsByOwnerOutput>
    {
        void WishlistsNotFoundForTheOwner(Owner owner, Visibility visibility);
    }
}
