using FluentValidation.TestHelper;
using System;
using Xunit;

namespace TreniniDotNet.Application.Boundaries.Collection.GetCollectionByOwner
{
    public class GetCollectionByOwnerInputValidatorTests
    {
        private GetCollectionByOwnerInputValidator Validator { get; }

        public GetCollectionByOwnerInputValidatorTests()
        {
            Validator = new GetCollectionByOwnerInputValidator();
        }

        [Fact]
        public void GetCollectionByOwnerInputValidator_ShouldFailToValidate_WhenOwnerIsNull()
        {
            var input = new GetCollectionByOwnerInput(Guid.Empty, null);

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Owner);
        }

        [Fact]
        public void GetCollectionByOwnerInputValidator_ShouldFailToValidate_WhenOwnerIsEmpty()
        {
            var input = new GetCollectionByOwnerInput(Guid.Empty, "     ");

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Owner);
        }

        [Fact]
        public void GetCollectionByOwnerInputValidator_ShouldValidateValidInputs()
        {
            var input = new GetCollectionByOwnerInput(Guid.NewGuid(), "George");

            var result = Validator.TestValidate(input);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
