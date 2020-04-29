using FluentValidation.TestHelper;
using TreniniDotNet.Common;
using Xunit;
using static TreniniDotNet.Application.TestInputs.Catalog.CatalogInputs;

namespace TreniniDotNet.Application.Catalog.CatalogItems.EditCatalogItem
{
    public class EditCatalogItemInputValidatorTests
    {
        private EditCatalogItemInputValidator Validator { get; }

        public EditCatalogItemInputValidatorTests()
        {
            Validator = new EditCatalogItemInputValidator();
        }

        [Fact]
        public void EditCatalogItemInputValidator_ShouldFailToValidateEmptyInputs()
        {
            var result = Validator.TestValidate(NewEditCatalogItemInput.Empty);

            result.ShouldHaveValidationErrorFor(x => x.Slug);
        }

        [Fact]
        public void EditCatalogItemInputValidator_ShouldValidateModifiedValues()
        {
            var input = NewEditCatalogItemInput.With(
                ItemSlug: Slug.Of("acme-12345"),
                ItemNumber: "");

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Values.ItemNumber);
        }
    }
}
