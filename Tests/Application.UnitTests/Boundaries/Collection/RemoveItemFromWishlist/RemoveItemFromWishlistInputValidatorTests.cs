using FluentValidation.TestHelper;
using System;
using TreniniDotNet.Application.TestInputs.Collection;
using Xunit;

namespace TreniniDotNet.Application.Boundaries.Collection.RemoveItemFromWishlist
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
            var input = CollectionInputs.RemoveItemFromWishlist.With(
                Id: Guid.NewGuid(),
                ItemId: Guid.NewGuid());

            var result = Validator.TestValidate(input);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void RemoveItemFromWishlistInput_ShouldFailValidation_WhenEmpty()
        {
            var input = CollectionInputs.RemoveItemFromWishlist.Empty;

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Id);
            result.ShouldHaveValidationErrorFor(x => x.ItemId);
        }
    }
}
