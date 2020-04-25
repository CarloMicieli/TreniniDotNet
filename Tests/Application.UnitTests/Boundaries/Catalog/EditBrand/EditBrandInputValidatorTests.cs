using FluentValidation.TestHelper;
using Xunit;
using static TreniniDotNet.Application.TestInputs.Catalog.CatalogInputs;

namespace TreniniDotNet.Application.Boundaries.Catalog.EditBrand
{
    public class EditBrandInputValidatorTests
    {
        private readonly EditBrandInputValidator Validator;

        public EditBrandInputValidatorTests()
        {
            Validator = new EditBrandInputValidator();
        }

        [Fact]
        public void EditBrandInput_ShouldPassValidation_WhenEmpty()
        {
            var input = NewEditBrandInput.Empty;

            var result = Validator.TestValidate(input);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void EditBrandInput_ShouldValidateBrandType()
        {
            var input = NewEditBrandInput.With(BrandType: "--invalid--");

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.BrandType);
        }

        [Fact]
        public void EditBrandInput_ShouldValidateEmailAddress()
        {
            var input = NewEditBrandInput.With(EmailAddress: "--invalid--");

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.EmailAddress);
        }

        [Fact]
        public void EditBrandInput_ShouldValidateWebsiteUrl()
        {
            var input = NewEditBrandInput.With(WebsiteUrl: "--invalid--");

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.WebsiteUrl);
        }
    }
}
