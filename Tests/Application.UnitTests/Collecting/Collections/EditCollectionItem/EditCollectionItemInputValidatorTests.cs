using System;
using FluentValidation.TestHelper;
using TreniniDotNet.Application.Collecting.Shared;
using TreniniDotNet.TestHelpers.Common;
using Xunit;

namespace TreniniDotNet.Application.Collecting.Collections.EditCollectionItem
{
    public class EditCollectionItemInputValidatorTests
    {
        private EditCollectionItemInputValidator Validator { get; }

        public EditCollectionItemInputValidatorTests()
        {
            Validator = new EditCollectionItemInputValidator();
        }

        [Fact]
        public void EditCollectionItemInput_ShouldFailValidation_WhenEmpty()
        {
            var input = NewEditCollectionItemInput.Empty;

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Id);
            result.ShouldHaveValidationErrorFor(x => x.ItemId);
        }

        [Fact]
        public void EditCollectionItemInput_ShouldFailValidation_WhenPriceIsZero()
        {
            var input = NewEditCollectionItemInput.With(price: NewPriceInput.Empty);

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Price.Value);
            result.ShouldHaveValidationErrorFor(x => x.Price.Currency);
        }

        [Fact]
        public void EditCollectionItemInput_ShouldFailValidation_WhenPriceIsNegative()
        {
            var input = NewEditCollectionItemInput.With(price: NewPriceInput.With(-10M));

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Price.Value);
            result.ShouldHaveValidationErrorFor(x => x.Price.Currency);
        }

        [Fact]
        public void EditCollectionItemInput_ShouldFailValidation_WhenConditionIsNotValid()
        {
            var input = NewEditCollectionItemInput.With(condition: "--invalid--");

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Condition);
        }

        [Fact]
        public void EditCollectionItemInput_ShouldFailValidation_WhenNotesAreTooLong()
        {
            var input = NewEditCollectionItemInput.With(
                notes: RandomString.WithLengthOf(151));

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Notes);
        }

        [Fact]
        public void EditCollectionItemInput_ShouldFailValidation_WhenShopIsTooLong()
        {
            var input = NewEditCollectionItemInput.With(
                shop: RandomString.WithLengthOf(51));

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Shop);
        }

        [Fact]
        public void EditCollectionItemInput_ShouldSucceedsValidation_WhenValid()
        {
            var input = NewEditCollectionItemInput.With(
                id: Guid.NewGuid(),
                itemId: Guid.NewGuid(),
                price: NewPriceInput.With(10M, "EUR"));

            var result = Validator.TestValidate(input);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}