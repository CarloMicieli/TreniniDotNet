using FluentValidation;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;

namespace TreniniDotNet.Application.Collecting.Wishlists.GetWishlistById
{
    public sealed class GetWishlistByIdInputValidator : AbstractUseCaseValidator<GetWishlistByIdInput>
    {
        public GetWishlistByIdInputValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
        }
    }
}
