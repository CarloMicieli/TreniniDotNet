using FluentValidation;

namespace TreniniDotNet.Application.Collecting.Wishlists.RemoveItemFromWishlist
{
    public sealed class RemoveItemFromWishlistInputValidator : AbstractValidator<RemoveItemFromWishlistInput>
    {
        public RemoveItemFromWishlistInputValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();

            RuleFor(x => x.ItemId)
                .NotEmpty();
        }
    }
}
