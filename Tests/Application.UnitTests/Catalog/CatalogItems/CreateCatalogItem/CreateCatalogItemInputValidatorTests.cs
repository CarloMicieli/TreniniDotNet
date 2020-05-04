using System.Collections.Generic;
using FluentValidation.TestHelper;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using Xunit;
using static TreniniDotNet.Application.Catalog.CatalogInputs;

namespace TreniniDotNet.Application.Catalog.CatalogItems.CreateCatalogItem
{
    public class CreateCatalogItemInputValidatorTests
    {
        private CreateCatalogItemInputValidator Validator { get; }

        public CreateCatalogItemInputValidatorTests()
        {
            Validator = new CreateCatalogItemInputValidator();
        }

        [Fact]
        public void CreateCatalogItemInputValidator_ShouldHaveNoError_WhenValid()
        {
            var rollingStockInput = NewRollingStockInput.With(
                epoch: "IV",
                category: Category.ElectricLocomotive.ToString(),
                railway: "FS",
                length: new LengthOverBufferInput(303M, null)
            );

            var input = new CreateCatalogItemInput(
                brand: "acme",
                itemNumber: "123456",
                description: "My first catalog item",
                prototypeDescription: null,
                modelDescription: null,
                powerMethod: "dc",
                scale: "h0",
                deliveryDate: null, available: false,
                rollingStocks: new List<RollingStockInput>() { rollingStockInput });

            var result = Validator.TestValidate(input);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void CreateCatalogItemInputValidator_ShouldHaveError_WhenBrandNameIsEmpty()
        {
            var input = NewCreateCatalogItemInput.With(
                brand: "",
                rollingStocks: EmptyRollingStocks());

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Brand);
        }

        [Fact]
        public void CreateCatalogItemInputValidator_ShouldHaveError_WhenItemNumberIsNull()
        {
            var input = NewCreateCatalogItemInput.With(
                itemNumber: null,
                rollingStocks: EmptyRollingStocks());

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.ItemNumber);
        }

        [Fact]
        public void CreateCatalogItemInputValidator_ShouldHaveError_WhenItemNumberIsTooShort()
        {
            var input = NewCreateCatalogItemInput.With(
                itemNumber: "123",
                rollingStocks: EmptyRollingStocks());

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.ItemNumber);
        }

        [Fact]
        public void CreateCatalogItemInputValidator_ShouldHaveError_WhenDescriptionIsNull()
        {
            var input = NewCreateCatalogItemInput.With(
                description: null,
                rollingStocks: EmptyRollingStocks());

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Description);
        }

        [Fact]
        public void CreateCatalogItemInputValidator_ShouldHaveError_WhenScaleIsNull()
        {
            var input = NewCreateCatalogItemInput.With(
                scale: null,
                rollingStocks: EmptyRollingStocks());

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Scale);
        }

        [Fact]
        public void CreateCatalogItemInputValidator_ShouldHaveError_WhenPowerMethodIsInvalid()
        {
            var input = NewCreateCatalogItemInput.With(
                powerMethod: "not valid",
                rollingStocks: EmptyRollingStocks());

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.PowerMethod);
        }

        [Fact]
        public void CreateCatalogItemInputValidator_ShouldHaveError_WhenHasNoRollingStocks()
        {
            var input = NewCreateCatalogItemInput.With(
                brand: "",
                itemNumber: "",
                powerMethod: "not valid",
                available: false,
                rollingStocks: EmptyRollingStocks());

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.RollingStocks);
        }

        [Fact]
        public void CreateCatalogItemInputValidator_ShouldHaveError_WhenAnyRollingStocksIsInvalid()
        {
            var input = NewCreateCatalogItemInput.With(
                brand: "",
                itemNumber: "",
                powerMethod: "not valid",
                available: false,
                rollingStocks: NullRollingStockInput());

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor("RollingStocks[0].Epoch");
            result.ShouldHaveValidationErrorFor("RollingStocks[0].Category");
            result.ShouldHaveValidationErrorFor("RollingStocks[0].Railway");
        }

        [Fact]
        public void CreateCatalogItemInputValidator_ShouldHaveError_WhenAnyRollingStocksHasNegativeLength()
        {
            var input = NewCreateCatalogItemInput.With(
                brand: "",
                itemNumber: "",
                powerMethod: "not valid",
                available: false,
                rollingStocks: ListOf(NewRollingStockInput.With(
                    epoch: "III",
                    category: Category.ElectricLocomotive.ToString(),
                    railway: "FS",
                    length: new LengthOverBufferInput(-10, -10))));

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor("RollingStocks[0].LengthOverBuffer.Millimeters");
            result.ShouldHaveValidationErrorFor("RollingStocks[0].LengthOverBuffer.Inches");
        }

        private IReadOnlyList<RollingStockInput> NullRollingStockInput()
        {
            return new List<RollingStockInput>()
            {
                NewRollingStockInput.Empty
            };
        }

        private IReadOnlyList<RollingStockInput> EmptyRollingStocks()
        {
            return new List<RollingStockInput>();
        }

        private IReadOnlyList<RollingStockInput> ListOf(params RollingStockInput[] values)
        {
            return new List<RollingStockInput>(values);
        }
    }
}
