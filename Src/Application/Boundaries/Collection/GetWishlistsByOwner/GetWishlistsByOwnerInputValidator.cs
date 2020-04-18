using FluentValidation;

namespace TreniniDotNet.Application.Boundaries.Collection.GetWishlistsByOwner
{
    public sealed class GetWishlistsByOwnerInputValidator : AbstractValidator<GetWishlistsByOwnerInput>
    {
        public GetWishlistsByOwnerInputValidator()
        {
            RuleFor(x => x.Owner)
                .NotEmpty()
                .NotNull();
        }
    }
}
