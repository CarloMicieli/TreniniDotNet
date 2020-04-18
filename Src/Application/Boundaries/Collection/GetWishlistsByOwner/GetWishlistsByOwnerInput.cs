using TreniniDotNet.Common.Interfaces;
using TreniniDotNet.Domain.Collection.ValueObjects;

namespace TreniniDotNet.Application.Boundaries.Collection.GetWishlistsByOwner
{
    public sealed class GetWishlistsByOwnerInput : IUseCaseInput
    {
        public GetWishlistsByOwnerInput(string owner, Visibility visibility)
        {
            Owner = owner;
            Visibility = visibility;
        }

        public string Owner { get; }

        public Visibility Visibility { get; }
    }
}
