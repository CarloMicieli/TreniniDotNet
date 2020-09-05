using System;
using FluentValidation.TestHelper;
using Xunit;

namespace TreniniDotNet.Application.Collecting.Shops.RemoveShopFromFavourites
{
    public class RemoveShopFromFavouritesInputValidatorTests
    {
        private RemoveShopFromFavouritesInputValidator Validator { get; }

        public RemoveShopFromFavouritesInputValidatorTests()
        {
            Validator = new RemoveShopFromFavouritesInputValidator();
        }

        [Fact]
        public void RemoveShopFromFavouritesInput_ShouldFailValidation_WhenEmpty()
        {
            var input = NewRemoveShopFromFavouritesInput.Empty;

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.ShopId);
            result.ShouldHaveValidationErrorFor(x => x.Owner);
        }

        [Fact]
        public void RemoveShopFromFavouritesInput_ShouldSucceedValidation_WhenValid()
        {
            var input = NewRemoveShopFromFavouritesInput.With("Owner", Guid.NewGuid());

            var result = Validator.TestValidate(input);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
