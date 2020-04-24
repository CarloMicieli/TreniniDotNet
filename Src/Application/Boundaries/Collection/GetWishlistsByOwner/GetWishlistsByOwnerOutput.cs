using System.Collections.Generic;
using TreniniDotNet.Domain.Collection.Shared;
using TreniniDotNet.Domain.Collection.ValueObjects;
using TreniniDotNet.Domain.Collection.Wishlists;

namespace TreniniDotNet.Application.Boundaries.Collection.GetWishlistsByOwner
{
    public sealed class GetWishlistsByOwnerOutput : IUseCaseOutput
    {
        public GetWishlistsByOwnerOutput(Owner owner, VisibilityCriteria visibility, IEnumerable<IWishlistInfo> wishlists)
        {
            Owner = owner;
            Visibility = visibility;
            Wishlists = wishlists;
        }

        public Owner Owner { get; }
        public VisibilityCriteria Visibility { get; }
        public IEnumerable<IWishlistInfo> Wishlists { get; }
    }
}
