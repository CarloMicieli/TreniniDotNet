using TreniniDotNet.Common.UseCases.Boundaries.Outputs.Ports;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.Wishlists;

namespace TreniniDotNet.Application.Collecting.Wishlists.GetWishlistsByOwner
{
    public interface IGetWishlistsByOwnerOutputPort : IStandardOutputPort<GetWishlistsByOwnerOutput>
    {
        void WishlistsNotFoundForTheOwner(Owner owner, VisibilityCriteria visibility);
    }
}
