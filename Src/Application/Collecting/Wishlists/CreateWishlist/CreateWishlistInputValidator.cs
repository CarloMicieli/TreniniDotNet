#nullable disable
using FluentValidation;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;
using TreniniDotNet.Domain.Collecting.Wishlists;

namespace TreniniDotNet.Application.Collecting.Wishlists.CreateWishlist
{
    public sealed class CreateWishlistInputValidator : AbstractUseCaseValidator<CreateWishlistInput>
    {
        public CreateWishlistInputValidator()
        {
            RuleFor(x => x.Owner)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.Visibility)
                .IsEnumName(typeof(Visibility), caseSensitive: false)
                .NotNull();

            RuleFor(x => x.ListName)
                .MaximumLength(50);

            RuleFor(x => x.Budget)
                .SetValidator(new BudgetInputValidator());
        }
    }
}
