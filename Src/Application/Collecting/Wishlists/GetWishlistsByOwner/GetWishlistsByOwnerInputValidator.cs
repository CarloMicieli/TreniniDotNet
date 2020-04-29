using FluentValidation;
using TreniniDotNet.Domain.Collecting.ValueObjects;

namespace TreniniDotNet.Application.Collecting.Wishlists.GetWishlistsByOwner
{
    public sealed class GetWishlistsByOwnerInputValidator : AbstractValidator<GetWishlistsByOwnerInput>
    {
        public GetWishlistsByOwnerInputValidator()
        {
            RuleFor(x => x.Owner)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.Visibility)
                .IsEnumName(typeof(VisibilityCriteria), caseSensitive: false);
        }
    }
}
