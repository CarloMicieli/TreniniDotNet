using TreniniDotNet.Common.UseCases.Interfaces;
using TreniniDotNet.Common.UseCases.Interfaces.Input;

namespace TreniniDotNet.Application.Collecting.Wishlists.CreateWishlist
{
    public sealed class CreateWishlistInput : IUseCaseInput
    {
        public CreateWishlistInput(string owner, string listName, string visibility)
        {
            Owner = owner;
            ListName = listName;
            Visibility = visibility;
        }

        public string Owner { get; }

        public string ListName { get; }

        public string Visibility { get; }
    }
}
