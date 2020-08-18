using FluentValidation;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;

namespace TreniniDotNet.Application.Collecting.Shops.RemoveShopFromFavourites
{
    public sealed class RemoveShopFromFavouritesInputValidator : AbstractUseCaseValidator<RemoveShopFromFavouritesInput>
    {
        public RemoveShopFromFavouritesInputValidator()
        {
            RuleFor(x => x.ShopId)
                .NotEmpty();

            RuleFor(x => x.Owner)
                .NotEmpty();
        }
    }
}
