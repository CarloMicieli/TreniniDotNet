using FluentValidation;

namespace TreniniDotNet.Application.Boundaries.Collection.GetWishlistById
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
