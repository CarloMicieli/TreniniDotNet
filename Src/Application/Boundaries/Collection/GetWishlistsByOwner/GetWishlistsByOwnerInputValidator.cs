using FluentValidation;
using TreniniDotNet.Domain.Collection.ValueObjects;

namespace TreniniDotNet.Application.Boundaries.Collection.GetWishlistsByOwner
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
