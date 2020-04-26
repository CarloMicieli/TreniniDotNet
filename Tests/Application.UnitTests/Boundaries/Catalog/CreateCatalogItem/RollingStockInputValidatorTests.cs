using FluentValidation.TestHelper;
using TreniniDotNet.Application.Boundaries.Common;
using Xunit;

namespace TreniniDotNet.Application.Boundaries.Catalog.CreateCatalogItem
{
    public class RollingStockInputValidatorTests
    {
        private readonly RollingStockInputValidator validator;

        public RollingStockInputValidatorTests()
        {
            validator = new RollingStockInputValidator();
        }

        [Fact]
        public void RollingStockInputValidator_ShouldHaveNoError_WhenValid()
        {
            var input = RollingStockInput();

            var result = validator.TestValidate(input);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void RollingStockInputValidator_ShouldHaveError_WhenEraIsInvalid()
        {
            var input = RollingStockInput(era: "invalid");

            var result = validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Era);
        }

        [Fact]
        public void RollingStockInputValidator_ShouldHaveError_WhenCategoryIsInvalid()
        {
            var input = RollingStockInput(category: "invalid");

            var result = validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Category);
        }

        [Fact]
        public void RollingStockInputValidator_ShouldHaveError_WhenControlIsInvalid()
        {
            var input = RollingStockInput(control: "invalid");

            var result = validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Control);
        }

        [Fact]
        public void RollingStockInputValidator_ShouldHaveError_WhenDccInterfaceIsInvalid()
        {
            var input = RollingStockInput(dccInterface: "invalid");

            var result = validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.DccInterface);
        }

        private static RollingStockInput RollingStockInput(
            decimal length = 210M,
            string era = "IV",
            string category = "ElectricLocomotive",
            string control = "DccReady",
            string dccInterface = "Nem652")
        {
            return new RollingStockInput(
                era: era,
                category: category,
                railway: "FS",
                className: "E 656",
                roadNumber: "E 656 210",
                typeName: null,
                length: new LengthOverBufferInput(length, null),
                control: control,
                dccInterface: dccInterface
            );
        }
    }
}