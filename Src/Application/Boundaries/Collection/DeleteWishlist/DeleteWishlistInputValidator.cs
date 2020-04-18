using FluentValidation;

namespace TreniniDotNet.Application.Boundaries.Collection.DeleteWishlist
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
