using Xunit;
using FluentAssertions;
using FluentValidation.TestHelper;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Application.Catalog.CatalogItems.EditRollingStock
{
    public class EditRollingStockInputValidatorTests
    {
        private EditRollingStockInputValidator Validator { get; }

        public EditRollingStockInputValidatorTests()
        {
            Validator = new EditRollingStockInputValidator();
        }

        [Fact]
        public void EditRollingStockInputValidator_ShouldFailToValidateEmptyInputs()
        {
            var input = CatalogInputs.NewEditRollingStockInput.Empty;

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Slug);
            result.ShouldHaveValidationErrorFor(x => x.RollingStockId);
        }

        [Fact]
        public void EditRollingStockInputValidator_ShouldPassValidationForValidInputs()
        {
            var input = CatalogInputs.NewEditRollingStockInput.With(
                Slug.Of("item-123456"),
                RollingStockId.NewId());

            var result = Validator.TestValidate(input);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void EditRollingStockInputValidator_ShouldValidateModifiedValues()
        {
            var input = CatalogInputs.NewEditRollingStockInput.With(
                Slug.Of("item-123456"),
                RollingStockId.NewId(),
                epoch: "Invalid",
                category: "Invalid",
                length: new LengthOverBufferInput(-10M, -10M),
                control: "Invalid",
                dccInterface: "Invalid",
                passengerCarType: "Invalid",
                serviceLevel: "Invalid");

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Values.Category);
            result.ShouldHaveValidationErrorFor(x => x.Values.Epoch);
            result.ShouldHaveValidationErrorFor(x => x.Values.Control);
            result.ShouldHaveValidationErrorFor(x => x.Values.DccInterface);
            result.ShouldHaveValidationErrorFor(x => x.Values.LengthOverBuffer.Inches);
            result.ShouldHaveValidationErrorFor(x => x.Values.LengthOverBuffer.Millimeters);
        }
    }
}
