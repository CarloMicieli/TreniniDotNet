using System;
using FluentValidation.TestHelper;
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
            var input = CollectingInputs.EditWishlistItem.With(
                id: Guid.NewGuid(),
                itemId: Guid.NewGuid());

            var result = Validator.TestValidate(input);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void EditWishlistItemInput_ShouldFailValidation_WhenItIsEmpty()
        {
            var input = CollectingInputs.EditWishlistItem.Empty;

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Id);
            result.ShouldHaveValidationErrorFor(x => x.ItemId);
        }

        [Fact]
        public void EditWishlistItemInput_ShouldFailValidation_WhenPriceIsNegative()
        {
            var input = CollectingInputs.EditWishlistItem.With(price: -1M);

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Price);
        }

        [Fact]
        public void EditWishlistItemInput_ShouldFailValidation_WhenPriceIsZero()
        {
            var input = CollectingInputs.EditWishlistItem.With(price: 0M);

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Price);
        }


        [Fact]
        public void EditWishlistItem_ShouldFailValidation_WhenPriorityIsInvalid()
        {
            var input = CollectingInputs.EditWishlistItem.With(
                priority: "--invalid--");

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Priority);
        }

        [Fact]
        public void EditWishlistItem_ShouldFailValidation_WhenNotesAreTooLong()
        {
            var input = CollectingInputs.EditWishlistItem.With(
                notes: RandomString.WithLengthOf(151));

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Notes);
        }
    }
}
