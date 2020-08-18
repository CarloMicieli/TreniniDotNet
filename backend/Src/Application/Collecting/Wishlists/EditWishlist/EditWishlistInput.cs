using System;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;

namespace TreniniDotNet.Application.Collecting.Wishlists.EditWishlist
{
    public sealed class EditWishlistInput : IUseCaseInput
    {
        public EditWishlistInput(Guid id, string owner, string? listName, string visibility, BudgetInput? budget)
        {
            Id = id;
            Owner = owner;
            ListName = listName;
            Visibility = visibility;
            Budget = budget;
        }

        public Guid Id { get; }

        public string Owner { get; }

        public string? ListName { get; }

        public string Visibility { get; }

        public BudgetInput? Budget { get; }
    }
}
