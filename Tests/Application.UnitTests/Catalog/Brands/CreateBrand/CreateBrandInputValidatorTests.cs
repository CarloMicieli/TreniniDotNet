using FluentValidation.TestHelper;
using Xunit;

namespace TreniniDotNet.Application.Catalog.Brands.CreateBrand
{
    public class CreateBrandInputValidatorTests
    {
        private CreateBrandInputValidator Validator { get; }

        public CreateBrandInputValidatorTests()
        {
            Validator = new CreateBrandInputValidator();
        }

        [Fact]
        public void CreateBrandInputValidator_ShouldHaveNoError_WhenEverythingIsValid()
        {
            var input = NewCreateBrandInput.With(
                name: "ACME",
                companyName: "Associazione Costruzioni Modellistiche Esatte",
                websiteUrl: "http://www.acmetreni.com",
                emailAddress: "mail@acmetreni.com",
                brandType: "Industrial");

            var result = Validator.TestValidate(input);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void CreateBrandInputValidator_ShouldHaveError_WhenNameIsNull()
        {
            var input = NewCreateBrandInput.Empty;

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void CreateBrandInputValidator_ShouldHaveError_WhenNameIsBlank()
        {
            var input = NewCreateBrandInput.With(name: "  ");

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void CreateBrandInputValidator_ShouldHaveError_WhenEmailIsNotValidMailAddress()
        {
            var input = NewCreateBrandInput.With(emailAddress: "not a mail");

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.EmailAddress);
        }

        [Fact]
        public void CreateBrandInputValidator_ShouldHaveError_WhenWebsiteUrlIsNotValidUri()
        {
            var input = NewCreateBrandInput.With(websiteUrl: "not an url");

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.WebsiteUrl);
        }

        [Fact]
        public void CreateBrandInputValidator_ShouldHaveError_WhenBrandKindIsNotValid()
        {
            var input = NewCreateBrandInput.With(brandType: "not a valid type");

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Kind);
        }
    }
}
