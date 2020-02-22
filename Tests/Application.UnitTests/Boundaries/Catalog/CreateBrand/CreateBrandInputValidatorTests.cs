using FluentValidation.TestHelper;
using Xunit;

namespace TreniniDotNet.Application.Boundaries.Catalog.CreateBrand
{
    public class CreateBrandInputValidatorTests
    {
        private readonly CreateBrandInputValidator validator;
        
        public CreateBrandInputValidatorTests()
        {
            validator = new CreateBrandInputValidator();
        }

        [Fact]
        public void CreateBrandInputValidator_ShouldHaveNoError_WhenEverythingIsValid()
        {
            var input = new CreateBrandInput(
                "ACME", 
                "Associazione Costruzioni Modellistiche Esatte", 
                "http://www.acmetreni.com", 
                "mail@acmetreni.com", 
                "Industrial");

            var result = validator.TestValidate(input);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void CreateBrandInputValidator_ShouldHaveError_WhenNameIsNull()
        {
            var input = new CreateBrandInput(null, null, null, null, null);

            var result = validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void CreateBrandInputValidator_ShouldHaveError_WhenNameIsBlank()
        {
            var input = new CreateBrandInput("  ", null, null, null, null);

            var result = validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void CreateBrandInputValidator_ShouldHaveError_WhenEmailIsNotValidMailAddress()
        {
            var input = new CreateBrandInput(null, null, null, "not a mail", null);

            var result = validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.EmailAddress);
        }

        [Fact]
        public void CreateBrandInputValidator_ShouldHaveError_WhenWebsiteUrlIsNotValidUri()
        {
            var input = new CreateBrandInput(null, null, "not an url", null, null);

            var result = validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.WebsiteUrl);
        }

        [Fact]
        public void CreateBrandInputValidator_ShouldHaveError_WhenBrandKindIsNotValid()
        {
            var input = new CreateBrandInput(null, null, null, null, "not a kind");

            var result = validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Kind);
        }
    }
}
