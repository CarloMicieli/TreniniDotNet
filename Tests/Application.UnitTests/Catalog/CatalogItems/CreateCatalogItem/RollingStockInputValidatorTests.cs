using FluentValidation.TestHelper;
using Xunit;

namespace TreniniDotNet.Application.Catalog.CatalogItems.CreateCatalogItem
{
    public class RollingStockInputValidatorTests
    {
        private RollingStockInputValidator Validator { get; }

        public RollingStockInputValidatorTests()
        {
            Validator = new RollingStockInputValidator();
        }

        [Fact]
        public void RollingStockInputValidator_ShouldHaveNoError_WhenValid()
        {
            var input = RollingStockInput();

            var result = Validator.TestValidate(input);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void RollingStockInputValidator_ShouldHaveError_WhenEraIsInvalid()
        {
            var input = RollingStockInput(era: "invalid");

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Epoch);
        }

        [Fact]
        public void RollingStockInputValidator_ShouldHaveError_WhenCategoryIsInvalid()
        {
            var input = RollingStockInput(category: "invalid");

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Category);
        }

        [Fact]
        public void RollingStockInputValidator_ShouldHaveError_WhenCouplersIsInvalid()
        {
            var input = RollingStockInput(couplers: "invalid");

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Couplers);
        }

        [Fact]
        public void RollingStockInputValidator_ShouldHaveError_WhenControlIsInvalid()
        {
            var input = RollingStockInput(control: "invalid");

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Control);
        }

        [Fact]
        public void RollingStockInputValidator_ShouldHaveError_WhenDccInterfaceIsInvalid()
        {
            var input = RollingStockInput(dccInterface: "invalid");

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.DccInterface);
        }

        private static RollingStockInput RollingStockInput(
            decimal length = 210M,
            string era = "IV",
            string category = "ElectricLocomotive",
            string control = "DccReady",
            string dccInterface = "Nem652",
            string couplers = null)
        {
            return CatalogInputs.NewRollingStockInput.With(
                epoch: era,
                category: category,
                railway: "FS",
                className: "E 656",
                roadNumber: "E 656 210",
                length: new LengthOverBufferInput(length, null),
                control: control,
                dccInterface: dccInterface,
                couplers: couplers
            );
        }
    }
}
