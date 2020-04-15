using FluentValidation.TestHelper;
using TreniniDotNet.Application.TestInputs.Collection;
using TreniniDotNet.TestHelpers.Common;
using Xunit;

namespace TreniniDotNet.Application.Boundaries.Collection.CreateWishlist
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
            var input = CollectionInputs.CreateWishlist.Empty;

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Owner);
            result.ShouldHaveValidationErrorFor(x => x.Visibility);
        }

        [Fact]
        public void CreateWishlistInput_ShouldFailValidation_WhenVisibilitIsNotValid()
        {
            var input = CollectionInputs.CreateWishlist.With(Owner: "George", Visibility: "--invalid--");

            var result = Validator.TestValidate(input);

            result.ShouldNotHaveValidationErrorFor(x => x.Owner);
            result.ShouldHaveValidationErrorFor(x => x.Visibility);
        }

        [Fact]
        public void CreateWishlistInput_ShouldFailValidation_WhenListNameIsTooLong()
        {
            var input = CollectionInputs.CreateWishlist.With(
                ListName: RandomString.WithLengthOf(51));

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.ListName);
        }
    }
}
