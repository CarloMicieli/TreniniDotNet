#nullable disable
using FluentValidation;
using TreniniDotNet.Application.Collecting.Shared;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;
using TreniniDotNet.Domain.Collecting.Wishlists;

namespace TreniniDotNet.Application.Collecting.Wishlists.AddItemToWishlist
{
    public sealed class AddItemToWishlistInputValidator : AbstractUseCaseValidator<AddItemToWishlistInput>
    {
        public AddItemToWishlistInputValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();

            RuleFor(x => x.CatalogItem)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.Price)
                .SetValidator(new PriceInputValidator());

            RuleFor(x => x.Priority)
                .IsEnumName(typeof(Priority), caseSensitive: false);

            RuleFor(x => x.Notes)
                .MaximumLength(150);
        }
    }
}
