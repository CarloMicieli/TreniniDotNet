using FluentValidation.TestHelper;
using TreniniDotNet.TestHelpers.Common;
using Xunit;

namespace TreniniDotNet.Application.Collecting.Wishlists.CreateWishlist
{
    public class CreateWishlistInputValidatorTests
    {
        private CreateWishlistInputValidator Validator { get; }

        public CreateWishlistInputValidatorTests()
        {
            Validator = new CreateWishlistInputValidator();
        }

        [Fact]
        public void CreateWishlistInput_ShouldFailValidation_WhenEmpty()
        {
            var input = NewCreateWishlistInput.Empty;

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Owner);
            result.ShouldHaveValidationErrorFor(x => x.Visibility);
        }

        [Fact]
        public void CreateWishlistInput_ShouldFailValidation_WhenVisibilityIsNotValid()
        {
            var input = NewCreateWishlistInput.With(owner: "George", visibility: "--invalid--");

            var result = Validator.TestValidate(input);

            result.ShouldNotHaveValidationErrorFor(x => x.Owner);
            result.ShouldHaveValidationErrorFor(x => x.Visibility);
        }

        [Fact]
        public void CreateWishlistInput_ShouldFailValidation_WhenListNameIsTooLong()
        {
            var input = NewCreateWishlistInput.With(
                listName: RandomString.WithLengthOf(51));

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.ListName);
        }
    }
}
