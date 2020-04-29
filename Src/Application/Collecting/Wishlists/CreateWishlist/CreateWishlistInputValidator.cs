using FluentValidation;
using TreniniDotNet.Domain.Collecting.ValueObjects;

namespace TreniniDotNet.Application.Collecting.Wishlists.CreateWishlist
{
    public sealed class CreateWishlistInputValidator : AbstractValidator<CreateWishlistInput>
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
        }
    }
}
