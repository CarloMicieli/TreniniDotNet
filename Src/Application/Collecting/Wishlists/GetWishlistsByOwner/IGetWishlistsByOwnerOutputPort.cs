using TreniniDotNet.Common.UseCases.Interfaces.Output;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.ValueObjects;

namespace TreniniDotNet.Application.Collecting.Wishlists.GetWishlistsByOwner
{
    public interface IGetWishlistsByOwnerOutputPort : IOutputPortStandard<GetWishlistsByOwnerOutput>
    {
        void WishlistsNotFoundForTheOwner(Owner owner, VisibilityCriteria visibility);
    }
}
