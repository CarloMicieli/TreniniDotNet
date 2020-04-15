using FluentValidation;
using TreniniDotNet.Domain.Collection.ValueObjects;

namespace TreniniDotNet.Application.Boundaries.Collection.CreateWishlist
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
