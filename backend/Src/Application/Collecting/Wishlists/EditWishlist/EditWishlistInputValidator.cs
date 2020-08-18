#nullable disable
using FluentValidation;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;
using TreniniDotNet.Domain.Collecting.Wishlists;

namespace TreniniDotNet.Application.Collecting.Wishlists.EditWishlist
{
    public sealed class EditWishlistInputValidator : AbstractUseCaseValidator<EditWishlistInput>
    {
        public EditWishlistInputValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();

            RuleFor(x => x.Owner)
                .NotEmpty();

            RuleFor(x => x.Visibility)
                .IsEnumName(typeof(Visibility), caseSensitive: false);

            RuleFor(x => x.ListName)
                .MaximumLength(50);

            RuleFor(x => x.Budget)
                .SetValidator(new BudgetInputValidator());
        }
    }
}
