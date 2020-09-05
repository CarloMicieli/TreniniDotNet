using TreniniDotNet.Common.UseCases.Boundaries.Inputs;

namespace TreniniDotNet.Application.Collecting.Wishlists.CreateWishlist
{
    public sealed class CreateWishlistInput : IUseCaseInput
    {
        public CreateWishlistInput(string owner, string? listName, string visibility, BudgetInput? budget)
        {
            Owner = owner;
            ListName = listName;
            Visibility = visibility;
            Budget = budget;
        }

        public string Owner { get; }

        public string? ListName { get; }

        public string Visibility { get; }

        public BudgetInput? Budget { get; }
    }
}
