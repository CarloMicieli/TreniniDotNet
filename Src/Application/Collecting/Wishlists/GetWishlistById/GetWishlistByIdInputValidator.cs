using FluentValidation;

namespace TreniniDotNet.Application.Collecting.Wishlists.GetWishlistById
{
    public sealed class GetWishlistByIdInputValidator : AbstractValidator<GetWishlistByIdInput>
    {
        public GetWishlistByIdInputValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
        }
    }
}
