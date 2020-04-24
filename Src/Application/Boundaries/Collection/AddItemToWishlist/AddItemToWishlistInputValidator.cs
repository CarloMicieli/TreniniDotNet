using FluentValidation;
using TreniniDotNet.Domain.Collection.ValueObjects;

namespace TreniniDotNet.Application.Boundaries.Collection.AddItemToWishlist
{
    public sealed class AddItemToWishlistInputValidator : AbstractValidator<AddItemToWishlistInput>
    {
        public AddItemToWishlistInputValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();

            RuleFor(x => x.CatalogItem)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.Price)
                .GreaterThan(0M);

            RuleFor(x => x.Priority)
                .IsEnumName(typeof(Priority), caseSensitive: false);

            RuleFor(x => x.Notes)
                .MaximumLength(150);
        }
    }
}
