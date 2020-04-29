using FluentValidation;
using TreniniDotNet.Domain.Collecting.ValueObjects;

namespace TreniniDotNet.Application.Collecting.Wishlists.EditWishlistItem
{
    public class EditWishlistItemInputValidator : AbstractValidator<EditWishlistItemInput>
    {
        public EditWishlistItemInputValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();

            RuleFor(x => x.ItemId)
                .NotEmpty();

            RuleFor(x => x.Price)
                .GreaterThan(0M);

            RuleFor(x => x.Priority)
                .IsEnumName(typeof(Priority), caseSensitive: false);

            RuleFor(x => x.Notes)
                .MaximumLength(150);
        }
    }
}
