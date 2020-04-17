using TreniniDotNet.Common.Interfaces;
using TreniniDotNet.Domain.Collection.ValueObjects;

namespace TreniniDotNet.Application.Boundaries.Collection.GetWishlistsList
{
    public sealed class GetWishlistsListInput : IUseCaseInput
    {
        public GetWishlistsListInput(string owner, Visibility visibility)
        {
            Owner = owner;
            Visibility = visibility;
        }

        public string Owner { get; }

        public Visibility Visibility { get; }
    }
}
