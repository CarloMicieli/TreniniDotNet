using FluentValidation;

namespace TreniniDotNet.Application.Collecting.Wishlists.DeleteWishlist
{
    public sealed class DeleteWishlistInputValidator : AbstractValidator<DeleteWishlistInput>
    {
        public DeleteWishlistInputValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
        }
    }
}
