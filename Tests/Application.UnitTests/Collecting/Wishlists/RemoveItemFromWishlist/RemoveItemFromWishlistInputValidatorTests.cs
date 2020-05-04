using System;
using FluentValidation.TestHelper;
using Xunit;

namespace TreniniDotNet.Application.Collecting.Wishlists.RemoveItemFromWishlist
{
    public class RemoveItemFromWishlistInputValidatorTests
    {
        private RemoveItemFromWishlistInputValidator Validator { get; }

        public RemoveItemFromWishlistInputValidatorTests()
        {
            Validator = new RemoveItemFromWishlistInputValidator();
        }

        [Fact]
        public void RemoveItemFromWishlistInput_ShouldSucceedValidation()
        {
            var input = CollectingInputs.RemoveItemFromWishlist.With(
                id: Guid.NewGuid(),
                itemId: Guid.NewGuid());

            var result = Validator.TestValidate(input);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void RemoveItemFromWishlistInput_ShouldFailValidation_WhenEmpty()
        {
            var input = CollectingInputs.RemoveItemFromWishlist.Empty;

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Id);
            result.ShouldHaveValidationErrorFor(x => x.ItemId);
        }
    }
}
