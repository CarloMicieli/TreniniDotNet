using FluentValidation;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;
using TreniniDotNet.Domain.Collecting.Wishlists;

namespace TreniniDotNet.Application.Collecting.Wishlists.GetWishlistsByOwner
{
    public sealed class GetWishlistsByOwnerInputValidator : AbstractUseCaseValidator<GetWishlistsByOwnerInput>
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
