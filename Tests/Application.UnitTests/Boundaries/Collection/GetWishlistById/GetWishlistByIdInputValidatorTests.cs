using FluentValidation.TestHelper;
using System;
using Xunit;

namespace TreniniDotNet.Application.Boundaries.Collection.GetWishlistById
{
    public class GetWishlistByIdInputValidatorTests
    {
        private GetWishlistByIdInputValidator Validator { get; }

        public GetWishlistByIdInputValidatorTests()
        {
            Validator = new GetWishlistByIdInputValidator();
        }

        [Fact]
        public void GetWishlistByIdInputValidator_ShouldFailToValidate_EmptyInputs()
        {
            var input = new GetWishlistByIdInput(null, Guid.Empty);

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Id);
        }

        [Fact]
        public void GetWishlistByIdInputValidator_ShouldValidate_ValidInputs()
        {
            var input = new GetWishlistByIdInput(null, Guid.NewGuid());

            var result = Validator.TestValidate(input);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
