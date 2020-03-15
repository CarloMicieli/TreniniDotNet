using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using Xunit;

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
                length: 303
            );

            var input = new CreateCatalogItemInput(
                brandName: "acme", 
                itemNumber: "123456",
                description: "My first catalog item",
                prototypeDescription: null,
                modelDescription: null,
                powerMethod: "dc",
                scale: "h0",
                rollingStocks: new List<RollingStockInput>() { rollingStockInput } );

            var result = validator.TestValidate(input);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void CreateCatalogItemInputValidator_ShouldHaveError_WhenBrandNameIsNull()
        {
            var input = new CreateCatalogItemInput(
                brandName: "", 
                itemNumber: "",
                description: null,
                prototypeDescription: null,
                modelDescription: null,
                powerMethod: null,
                scale: null,
                rollingStocks: EmptyRollingStocks());

            var result = validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.BrandName);
        }

        [Fact]
        public void CreateCatalogItemInputValidator_ShouldHaveError_WhenItemNumberIsNull()
        {
            var input = new CreateCatalogItemInput(
                brandName: "", 
                itemNumber: "",
                description: null,
                prototypeDescription: null,
                modelDescription: null,
                powerMethod: null,
                scale: null,
                rollingStocks: EmptyRollingStocks());

            var result = validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.ItemNumber);
        }
 
        [Fact]
        public void CreateCatalogItemInputValidator_ShouldHaveError_WhenItemNumberIsTooShort()
        {
            var input = new CreateCatalogItemInput(
                brandName: "", 
                itemNumber: "123",
                description: null,
                prototypeDescription: null,
                modelDescription: null,
                powerMethod: null,
                scale: null,
                rollingStocks: EmptyRollingStocks());

            var result = validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.ItemNumber);
        }

        [Fact]
        public void CreateCatalogItemInputValidator_ShouldHaveError_WhenDescriptionIsNull()
        {
            var input = new CreateCatalogItemInput(
                brandName: "", 
                itemNumber: "",
                description: null,
                prototypeDescription: null,
                modelDescription: null,
                powerMethod: null,
                scale: null,
                rollingStocks: EmptyRollingStocks());

            var result = validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Description);
        }

        [Fact]
        public void CreateCatalogItemInputValidator_ShouldHaveError_WhenScaleIsNull()
        {
            var input = new CreateCatalogItemInput(
                brandName: "", 
                itemNumber: "",
                description: null,
                prototypeDescription: null,
                modelDescription: null,
                powerMethod: null,
                scale: null,
                rollingStocks: EmptyRollingStocks());

            var result = validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Scale);
        }

        [Fact]
        public void CreateCatalogItemInputValidator_ShouldHaveError_WhenPowerMethodIsInvalid()
        {
            var input = new CreateCatalogItemInput(
                brandName: "", 
                itemNumber: "",
                description: null,
                prototypeDescription: null,
                modelDescription: null,
                powerMethod: "not valid",
                scale: null,
                rollingStocks: EmptyRollingStocks());

            var result = validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.PowerMethod);
        }

        [Fact]
        public void CreateCatalogItemInputValidator_ShouldHaveError_WhenHasNoRollingStocks()
        {
            var input = new CreateCatalogItemInput(
                brandName: "", 
                itemNumber: "",
                description: null,
                prototypeDescription: null,
                modelDescription: null,
                powerMethod: "not valid",
                scale: null,
                rollingStocks: EmptyRollingStocks());

            var result = validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.RollingStocks);
        }

        [Fact]
        public void CreateCatalogItemInputValidator_ShouldHaveError_WhenAnyRollingStocksIsInvalid()
        {
            var input = new CreateCatalogItemInput(
                brandName: "", 
                itemNumber: "",
                description: null,
                prototypeDescription: null,
                modelDescription: null,
                powerMethod: "not valid",
                scale: null,
                rollingStocks: NullRollingStockInput());

            var result = validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor("RollingStocks[0].Era");
            result.ShouldHaveValidationErrorFor("RollingStocks[0].Category");
            result.ShouldHaveValidationErrorFor("RollingStocks[0].Railway");
        }

        private IList<RollingStockInput> NullRollingStockInput()
        {
            return new List<RollingStockInput>() 
            { 
                new RollingStockInput(
                    era: "", 
                    category: "", 
                    railway: "", 
                    className: null, 
                    roadNumber: null,
                    length: null)
            };
        }

        private IList<RollingStockInput> EmptyRollingStocks()
        {
            return new List<RollingStockInput>();
        }
    }
}