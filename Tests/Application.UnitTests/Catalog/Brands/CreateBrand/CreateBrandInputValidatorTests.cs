using FluentValidation.TestHelper;
using Xunit;
using static TreniniDotNet.Application.Catalog.CatalogInputs;

namespace TreniniDotNet.Application.Catalog.Brands.CreateBrand
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
            var input = NewCreateBrandInput.With(
                Name: "ACME",
                CompanyName: "Associazione Costruzioni Modellistiche Esatte",
                WebsiteUrl: "http://www.acmetreni.com",
                EmailAddress: "mail@acmetreni.com",
                BrandType: "Industrial");

            var result = validator.TestValidate(input);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void CreateBrandInputValidator_ShouldHaveError_WhenNameIsNull()
        {
            var input = NewCreateBrandInput.Empty();

            var result = validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void CreateBrandInputValidator_ShouldHaveError_WhenNameIsBlank()
        {
            var input = NewCreateBrandInput.With(Name: "  ");

            var result = validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void CreateBrandInputValidator_ShouldHaveError_WhenEmailIsNotValidMailAddress()
        {
            var input = NewCreateBrandInput.With(EmailAddress: "not a mail");

            var result = validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.EmailAddress);
        }

        [Fact]
        public void CreateBrandInputValidator_ShouldHaveError_WhenWebsiteUrlIsNotValidUri()
        {
            var input = NewCreateBrandInput.With(WebsiteUrl: "not an url");

            var result = validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.WebsiteUrl);
        }

        [Fact]
        public void CreateBrandInputValidator_ShouldHaveError_WhenBrandKindIsNotValid()
        {
            var input = NewCreateBrandInput.With(BrandType: "not a valid type");

            var result = validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Kind);
        }
    }
}
