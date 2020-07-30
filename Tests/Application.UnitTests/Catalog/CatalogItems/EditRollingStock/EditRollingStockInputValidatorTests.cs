using FluentValidation.TestHelper;
using TreniniDotNet.Domain.Catalog.CatalogItems.RollingStocks;
using TreniniDotNet.SharedKernel.Slugs;
using Xunit;

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
            var input = NewEditRollingStockInput.Empty;

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Slug);
            result.ShouldHaveValidationErrorFor(x => x.RollingStockId);
        }

        [Fact]
        public void EditRollingStockInputValidator_ShouldPassValidationForValidInputs()
        {
            var input = NewEditRollingStockInput.With(
                Slug.Of("item-123456"),
                RollingStockId.NewId());

            var result = Validator.TestValidate(input);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void EditRollingStockInputValidator_ShouldValidateModifiedValues()
        {
            var input = NewEditRollingStockInput.With(
                Slug.Of("item-123456"),
                RollingStockId.NewId(),
                epoch: "Invalid",
                category: "Invalid",
                length: new LengthOverBufferInput(-10M, -10M),
                control: "Invalid",
                dccInterface: "Invalid",
                passengerCarType: "Invalid",
                serviceLevel: "Invalid",
                couplers: "Invalid");

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Values.Category);
            result.ShouldHaveValidationErrorFor(x => x.Values.Epoch);
            result.ShouldHaveValidationErrorFor(x => x.Values.Control);
            result.ShouldHaveValidationErrorFor(x => x.Values.DccInterface);
            result.ShouldHaveValidationErrorFor(x => x.Values.Couplers);
            result.ShouldHaveValidationErrorFor(x => x.Values.LengthOverBuffer.Inches);
            result.ShouldHaveValidationErrorFor(x => x.Values.LengthOverBuffer.Millimeters);
        }
    }
}
