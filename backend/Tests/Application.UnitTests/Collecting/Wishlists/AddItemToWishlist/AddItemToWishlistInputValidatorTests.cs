using System;
using FluentValidation.TestHelper;
using TreniniDotNet.Application.Collecting.Shared;
using TreniniDotNet.TestHelpers.Common;
using Xunit;

namespace TreniniDotNet.Application.Collecting.Wishlists.AddItemToWishlist
{
    public class AddItemToWishlistInputValidatorTests
    {
        private AddItemToWishlistInputValidator Validator { get; }

        public AddItemToWishlistInputValidatorTests()
        {
            Validator = new AddItemToWishlistInputValidator();
        }

        [Fact]
        public void AddItemToWishlistInput_ShouldSucceedValidation()
        {
            var input = NewAddItemToWishlistInput.With(
                id: Guid.NewGuid(),
                catalogItem: "acme-123456");

            var result = Validator.TestValidate(input);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void AddItemToWishlistInput_ShouldFailValidation_WhenItIsEmpty()
        {
            var input = NewAddItemToWishlistInput.Empty;

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Id);
            result.ShouldHaveValidationErrorFor(x => x.CatalogItem);
        }

        [Fact]
        public void AddItemToWishlistInput_ShouldFailValidation_WhenPriceIsNegative()
        {
            var input = NewAddItemToWishlistInput.With(price: new PriceInput(-1M, "EUR"));

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Price.Value);
        }

        [Fact]
        public void AddItemToWishlistInput_ShouldFailValidation_WhenPriceIsZero()
        {
            var input = NewAddItemToWishlistInput.With(price: new PriceInput(0M, "EUR"));

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Price.Value);
        }

        [Fact]
        public void AddItemToWishlistInput_ShouldFailValidation_WhenItemNumberIsTooLong()
        {
            var input = NewAddItemToWishlistInput.With(
                catalogItem: RandomString.WithLengthOf(51));

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.CatalogItem);
        }

        [Fact]
        public void AddItemToWishlistInput_ShouldFailValidation_WhenPriorityIsInvalid()
        {
            var input = NewAddItemToWishlistInput.With(
                priority: "--invalid--");

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Priority);
        }

        [Fact]
        public void AddItemToWishlistInput_ShouldFailValidation_WhenNotesAreTooLong()
        {
            var input = NewAddItemToWishlistInput.With(
                notes: RandomString.WithLengthOf(151));

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Notes);
        }
    }
}
