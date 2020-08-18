using System.Collections.Generic;
using TreniniDotNet.Common.UseCases.Boundaries.Outputs;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.Wishlists;

namespace TreniniDotNet.Application.Collecting.Wishlists.GetWishlistsByOwner
{
    public sealed class GetWishlistsByOwnerOutput : IUseCaseOutput
    {
        public GetWishlistsByOwnerOutput(Owner owner, VisibilityCriteria visibility, IEnumerable<Wishlist> wishlists)
        {
            Owner = owner;
            Visibility = visibility;
            Wishlists = wishlists;
        }

        public Owner Owner { get; }
        public VisibilityCriteria Visibility { get; }
        public IEnumerable<Wishlist> Wishlists { get; }
    }
}
