using TreniniDotNet.Common.UseCases.Interfaces;
using TreniniDotNet.Common.UseCases.Interfaces.Input;

namespace TreniniDotNet.Application.Collecting.Wishlists.GetWishlistsByOwner
{
    public sealed class GetWishlistsByOwnerInput : IUseCaseInput
    {
        public GetWishlistsByOwnerInput(string owner, string? visibility, bool userIsOwner = false)
        {
            Owner = owner;
            Visibility = visibility;
            UserIsOwner = userIsOwner;
        }

        public string Owner { get; }

        public string? Visibility { get; }

        public bool UserIsOwner { get; }
    }
}
