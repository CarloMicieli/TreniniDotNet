using System;
using FluentValidation.TestHelper;
using TreniniDotNet.Application.Collecting.Shared;
using TreniniDotNet.TestHelpers.Common;
using Xunit;

namespace TreniniDotNet.Application.Collecting.Wishlists.EditWishlistItem
{
    public class EditWishlistItemInputValidatorTests
    {
        private EditWishlistItemInputValidator Validator { get; }

        public EditWishlistItemInputValidatorTests()
        {
            Validator = new EditWishlistItemInputValidator();
        }

        [Fact]
        public void EditWishlistItemInput_ShouldSucceedValidation()
        {
            var input = NewEditWishlistItemInput.With(
                id: Guid.NewGuid(),
                itemId: Guid.NewGuid());

            var result = Validator.TestValidate(input);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void EditWishlistItemInput_ShouldFailValidation_WhenItIsEmpty()
        {
            var input = NewEditWishlistItemInput.Empty;

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Id);
            result.ShouldHaveValidationErrorFor(x => x.ItemId);
        }

        [Fact]
        public void EditWishlistItemInput_ShouldFailValidation_WhenPriceIsNegative()
        {
            var input = NewEditWishlistItemInput.With(price: NewPriceInput.With(-1M));

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Price.Value);
        }

        [Fact]
        public void EditWishlistItemInput_ShouldFailValidation_WhenPriceIsZero()
        {
            var input = NewEditWishlistItemInput.With(price: NewPriceInput.With(0M));

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Price.Value);
        }


        [Fact]
        public void EditWishlistItem_ShouldFailValidation_WhenPriorityIsInvalid()
        {
            var input = NewEditWishlistItemInput.With(
                priority: "--invalid--");

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Priority);
        }

        [Fact]
        public void EditWishlistItem_ShouldFailValidation_WhenNotesAreTooLong()
        {
            var input = NewEditWishlistItemInput.With(
                notes: RandomString.WithLengthOf(151));

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Notes);
        }
    }
}
