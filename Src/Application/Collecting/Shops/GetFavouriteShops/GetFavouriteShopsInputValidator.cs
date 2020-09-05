using FluentValidation;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;

namespace TreniniDotNet.Application.Collecting.Shops.GetFavouriteShops
{
    public sealed class GetFavouriteShopsInputValidator : AbstractUseCaseValidator<GetFavouriteShopsInput>
    {
        public GetFavouriteShopsInputValidator()
        {
            RuleFor(x => x.Owner)
                .NotEmpty();
        }
    }
}
