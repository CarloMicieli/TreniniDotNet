using FluentValidation.TestHelper;
using System.Collections.Generic;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using Xunit;
using static TreniniDotNet.Application.TestInputs.Catalog.CatalogInputs;

namespace TreniniDotNet.Application.Boundaries.Catalog.CreateCatalogItem
{
    public class CreateCatalogItemInputValidatorTests
    {
        private readonly CreateCatalogItemInputValidator validator;

        public CreateCatalogItemInputValidatorTests()
        {
            validator = new CreateCatalogItemInputValidator();
        }

        [Fact]
        public void CreateCatalogItemInputValidator_ShouldHaveNoError_WhenValid()
        {
            var rollingStockInput = new RollingStockInput(
                era: "IV",
                category: Category.ElectricLocomotive.ToString(),
                railway: "FS",
                className: null,
                roadNumber: null,
                typeName: null,
                length: new LengthOverBufferInput(303M, null),
                control: null,
                dccInterface: null
            );

            var input = new CreateCatalogItemInput(
                brandName: "acme",
                itemNumber: "123456",
                description: "My first catalog item",
                prototypeDescription: null,
                modelDescription: null,
                powerMethod: "dc",
                scale: "h0",
                deliveryDate: null, available: false,
                rollingStocks: new List<RollingStockInput>() { rollingStockInput });

            var result = validator.TestValidate(input);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void CreateCatalogItemInputValidator_ShouldHaveError_WhenBrandNameIsEmpty()
        {
            var input = NewCatalogItemInput.With(
                BrandName: "",
                RollingStocks: EmptyRollingStocks());

            var result = validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Brand);
        }

        [Fact]
        public void CreateCatalogItemInputValidator_ShouldHaveError_WhenItemNumberIsNull()
        {
            var input = NewCatalogItemInput.With(
                ItemNumber: null,
                RollingStocks: EmptyRollingStocks());

            var result = validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.ItemNumber);
        }

        [Fact]
        public void CreateCatalogItemInputValidator_ShouldHaveError_WhenItemNumberIsTooShort()
        {
            var input = NewCatalogItemInput.With(
                ItemNumber: "123",
                RollingStocks: EmptyRollingStocks());

            var result = validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.ItemNumber);
        }

        [Fact]
        public void CreateCatalogItemInputValidator_ShouldHaveError_WhenDescriptionIsNull()
        {
            var input = NewCatalogItemInput.With(
                Description: null,
                RollingStocks: EmptyRollingStocks());

            var result = validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Description);
        }

        [Fact]
        public void CreateCatalogItemInputValidator_ShouldHaveError_WhenScaleIsNull()
        {
            var input = NewCatalogItemInput.With(
                Scale: null,
                RollingStocks: EmptyRollingStocks());

            var result = validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Scale);
        }

        [Fact]
        public void CreateCatalogItemInputValidator_ShouldHaveError_WhenPowerMethodIsInvalid()
        {
            var input = NewCatalogItemInput.With(
                PowerMethod: "not valid",
                RollingStocks: EmptyRollingStocks());

            var result = validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.PowerMethod);
        }

        [Fact]
        public void CreateCatalogItemInputValidator_ShouldHaveError_WhenHasNoRollingStocks()
        {
            var input = NewCatalogItemInput.With(
                BrandName: "",
                ItemNumber: "",
                PowerMethod: "not valid",
                Available: false,
                RollingStocks: EmptyRollingStocks());

            var result = validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.RollingStocks);
        }

        [Fact]
        public void CreateCatalogItemInputValidator_ShouldHaveError_WhenAnyRollingStocksIsInvalid()
        {
            var input = NewCatalogItemInput.With(
                BrandName: "",
                ItemNumber: "",
                PowerMethod: "not valid",
                Available: false,
                RollingStocks: NullRollingStockInput());

            var result = validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor("RollingStocks[0].Era");
            result.ShouldHaveValidationErrorFor("RollingStocks[0].Category");
            result.ShouldHaveValidationErrorFor("RollingStocks[0].Railway");
        }

        [Fact]
        public void CreateCatalogItemInputValidator_ShouldHaveError_WhenAnyRollingStocksHasNegativeLength()
        {
            var input = NewCatalogItemInput.With(
                BrandName: "",
                ItemNumber: "",
                PowerMethod: "not valid",
                Available: false,
                RollingStocks: ListOf(NewRollingStockInput.With(
                    Era: "III",
                    Category: Category.ElectricLocomotive.ToString(),
                    Railway: "FS",
                    Length: new LengthOverBufferInput(-10, -10))));

            var result = validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor("RollingStocks[0].Length.Millimeters");
            result.ShouldHaveValidationErrorFor("RollingStocks[0].Length.Inches");
        }

        private IList<RollingStockInput> NullRollingStockInput()
        {
            return new List<RollingStockInput>()
            {
                NewRollingStockInput.Empty
            };
        }

        private IList<RollingStockInput> EmptyRollingStocks()
        {
            return new List<RollingStockInput>();
        }

        private IList<RollingStockInput> ListOf(params RollingStockInput[] values)
        {
            return new List<RollingStockInput>(values);
        }
    }
}