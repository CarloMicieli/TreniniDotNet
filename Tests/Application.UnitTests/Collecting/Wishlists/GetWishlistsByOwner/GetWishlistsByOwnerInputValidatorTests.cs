using FluentValidation.TestHelper;
using TreniniDotNet.Domain.Collecting.ValueObjects;
using Xunit;

namespace TreniniDotNet.Application.Collecting.Wishlists.GetWishlistsByOwner
{
    public class GetWishlistsByOwnerInputValidatorTests
    {
        private GetWishlistsByOwnerInputValidator Validator { get; }

        public GetWishlistsByOwnerInputValidatorTests()
        {
            Validator = new GetWishlistsByOwnerInputValidator();
        }

        [Fact]
        public void GetWishlistsByOwnerInputValidator_ShouldFailToValidateEmptyInputs()
        {
            var input = new GetWishlistsByOwnerInput("", Visibility.Public.ToString());

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Owner);
        }

        [Fact]
        public void GetWishlistsByOwnerInputValidator_ShouldValidateInputs()
        {
            var input = new GetWishlistsByOwnerInput("George", Visibility.Public.ToString());

            var result = Validator.TestValidate(input);

            result.ShouldNotHaveValidationErrorFor(x => x.Owner);
        }
    }
}
