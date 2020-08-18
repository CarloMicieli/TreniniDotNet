using FluentValidation;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;

namespace TreniniDotNet.Application.Collecting.Wishlists.DeleteWishlist
{
    public sealed class DeleteWishlistInputValidator : AbstractUseCaseValidator<DeleteWishlistInput>
    {
        public DeleteWishlistInputValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
        }
    }
}
