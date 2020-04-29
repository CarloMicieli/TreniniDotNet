using FluentValidation.TestHelper;
using TreniniDotNet.Common;
using Xunit;
using static TreniniDotNet.Application.TestInputs.Catalog.CatalogInputs;

namespace TreniniDotNet.Application.Catalog.Brands.EditBrand
{
    public class EditBrandInputValidatorTests
    {
        private readonly EditBrandInputValidator Validator;

        public EditBrandInputValidatorTests()
        {
            Validator = new EditBrandInputValidator();
        }

        [Fact]
        public void EditBrandInput_ShouldFailValidation_WhenEmpty()
        {
            var input = NewEditBrandInput.Empty;

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.BrandSlug);
        }

        [Fact]
        public void EditBrandInput_ShouldValidateOriginalBrandSlug()
        {
            var input = NewEditBrandInput.With(BrandSlug: Slug.Empty);

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.BrandSlug);
        }

        [Fact]
        public void EditBrandInput_ShouldValidateBrandType()
        {
            var input = NewEditBrandInput.With(BrandType: "--invalid--");

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Values.BrandType);
        }

        [Fact]
        public void EditBrandInput_ShouldValidateEmailAddress()
        {
            var input = NewEditBrandInput.With(EmailAddress: "--invalid--");

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Values.EmailAddress);
        }

        [Fact]
        public void EditBrandInput_ShouldValidateWebsiteUrl()
        {
            var input = NewEditBrandInput.With(WebsiteUrl: "--invalid--");

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Values.WebsiteUrl);
        }
    }
}
