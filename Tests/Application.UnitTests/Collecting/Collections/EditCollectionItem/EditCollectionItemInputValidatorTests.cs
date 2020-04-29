using System;
using FluentValidation.TestHelper;
using TreniniDotNet.Application.TestInputs.Collecting;
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
            var input = CollectingInputs.EditCollectionItem.Empty;

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Id);
            result.ShouldHaveValidationErrorFor(x => x.ItemId);
        }

        [Fact]
        public void EditCollectionItemInput_ShouldFailValidation_WhenPriceIsZero()
        {
            var input = CollectingInputs.EditCollectionItem.With(Price: 0M);

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Price);
        }

        [Fact]
        public void EditCollectionItemInput_ShouldFailValidation_WhenPriceIsNegative()
        {
            var input = CollectingInputs.EditCollectionItem.With(Price: -10M);

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Price);
        }

        [Fact]
        public void EditCollectionItemInput_ShouldFailValidation_WhenConditionIsNotValid()
        {
            var input = CollectingInputs.EditCollectionItem.With(Condition: "--invalid--");

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Condition);
        }

        [Fact]
        public void EditCollectionItemInput_ShouldFailValidation_WhenNotesAreTooLong()
        {
            var input = CollectingInputs.EditCollectionItem.With(
                Notes: RandomString.WithLengthOf(151));

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Notes);
        }

        [Fact]
        public void EditCollectionItemInput_ShouldFailValidation_WhenShopIsTooLong()
        {
            var input = CollectingInputs.EditCollectionItem.With(
                Shop: RandomString.WithLengthOf(51));

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Shop);
        }

        [Fact]
        public void EditCollectionItemInput_ShouldSucceedsValidation_WhenValid()
        {
            var input = CollectingInputs.EditCollectionItem.With(
                Id: Guid.NewGuid(),
                ItemId: Guid.NewGuid(),
                Price: 10M);

            var result = Validator.TestValidate(input);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
