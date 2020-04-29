using FluentValidation.TestHelper;
using TreniniDotNet.Application.TestInputs.Collecting;
using Xunit;

namespace TreniniDotNet.Application.Collecting.Shops.CreateShop
{
    public class CreateShopInputValidatorTests
    {
        private CreateShopInputValidator Validator { get; }

        public CreateShopInputValidatorTests()
        {
            Validator = new CreateShopInputValidator();
        }

        [Fact]
        public void CreateShopInput_ShouldFailValidation_WhenEmpty()
        {
            var input = CollectingInputs.CreateShop.Empty;

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void CreateShopInput_ShouldFailValidation_WhenWebsiteUrlIsInvalid()
        {
            var input = CollectingInputs.CreateShop.With(Name: "Shop Name", WebsiteUrl: "--invalid--");

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.WebsiteUrl);
        }

        [Fact]
        public void CreateShopInput_ShouldFailValidation_WhenEmailAddressIsInvalid()
        {
            var input = CollectingInputs.CreateShop.With(Name: "Shop Name", EmailAddress: "--invalid--");

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.EmailAddress);
        }

        [Fact]
        public void CreateShopInput_ShouldFailValidation_WhenPhoneNumberIsInvalid()
        {
            var input = CollectingInputs.CreateShop.With(Name: "Shop Name", PhoneNumber: "--invalid--");

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.PhoneNumber);
        }

        [Fact]
        public void CreateShopInput_ShouldSucceedValidation_WhenValid()
        {
            var input = CollectingInputs.CreateShop.With(Name: "Shop Name");

            var result = Validator.TestValidate(input);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
