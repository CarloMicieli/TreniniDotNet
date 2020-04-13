using FluentValidation.TestHelper;
using Xunit;

namespace TreniniDotNet.Application.Boundaries.Collection.RemoveShopFromFavourites
{
    public class RemoveShopFromFavouritesInputValidatorTests
    {
        private RemoveShopFromFavouritesInputValidator Validator { get; }

        public RemoveShopFromFavouritesInputValidatorTests()
        {
            Validator = new RemoveShopFromFavouritesInputValidator();
        }
    }
}
