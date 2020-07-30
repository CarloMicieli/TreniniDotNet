using FluentValidation.TestHelper;
using Xunit;

namespace TreniniDotNet.Application.Collecting.Shops.GetFavouriteShops
{
    public class GetFavouriteShopsInputValidatorTests
    {
        private GetFavouriteShopsInputValidator Validator { get; }

        public GetFavouriteShopsInputValidatorTests()
        {
            Validator = new GetFavouriteShopsInputValidator();
        }

        [Fact]
        public void GetFavouriteShopsInput_ShouldFailValidation_WhenEmpty()
        {
            var input = NewGetFavouriteShopsInput.Empty;

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Owner);
        }

        [Fact]
        public void GetFavouriteShopsInput_ShouldSucceedValidation_WhenValid()
        {
            var input = NewGetFavouriteShopsInput.With("Owner");

            var result = Validator.TestValidate(input);

            result.ShouldNotHaveAnyValidationErrors();
        }

    }
}
