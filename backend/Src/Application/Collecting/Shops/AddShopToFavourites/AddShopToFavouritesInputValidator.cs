using FluentValidation;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;

namespace TreniniDotNet.Application.Collecting.Shops.AddShopToFavourites
{
    public sealed class AddShopToFavouritesInputValidator : AbstractUseCaseValidator<AddShopToFavouritesInput>
    {
        public AddShopToFavouritesInputValidator()
        {
            RuleFor(x => x.ShopId)
                .NotEmpty();

            RuleFor(x => x.Owner)
                .NotEmpty();
        }
    }
}
