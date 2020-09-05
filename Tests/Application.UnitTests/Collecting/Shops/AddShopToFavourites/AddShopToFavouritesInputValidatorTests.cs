using System;
using FluentValidation.TestHelper;
using Xunit;

namespace TreniniDotNet.Application.Collecting.Shops.AddShopToFavourites
{
    public class AddShopToFavouritesInputValidatorTests
    {
        private AddShopToFavouritesInputValidator Validator { get; }

        public AddShopToFavouritesInputValidatorTests()
        {
            Validator = new AddShopToFavouritesInputValidator();
        }

        [Fact]
        public void AddShopToFavouritesInput_ShouldFailValidation_WhenEmpty()
        {
            var input = NewAddShopToFavouritesInput.Empty;

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.ShopId);
            result.ShouldHaveValidationErrorFor(x => x.Owner);
        }

        [Fact]
        public void AddShopToFavouritesInput_ShouldSucceedValidation_WhenValid()
        {
            var input = NewAddShopToFavouritesInput.With("Owner", Guid.NewGuid());

            var result = Validator.TestValidate(input);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
