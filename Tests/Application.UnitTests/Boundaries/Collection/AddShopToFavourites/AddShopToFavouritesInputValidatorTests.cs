using FluentValidation.TestHelper;
using Xunit;

namespace TreniniDotNet.Application.Boundaries.Collection.AddShopToFavourites
{
    public class AddShopToFavouritesInputValidatorTests
    {
        private AddShopToFavouritesInputValidator Validator { get; }

        public AddShopToFavouritesInputValidatorTests()
        {
            Validator = new AddShopToFavouritesInputValidator();
        }
    }
}
