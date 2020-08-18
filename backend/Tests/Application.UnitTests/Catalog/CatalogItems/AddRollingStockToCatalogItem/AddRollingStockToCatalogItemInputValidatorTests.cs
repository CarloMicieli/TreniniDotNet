using FluentValidation.TestHelper;
using TreniniDotNet.Domain.Catalog.CatalogItems.RollingStocks;
using TreniniDotNet.SharedKernel.Slugs;
using Xunit;

namespace TreniniDotNet.Application.Catalog.CatalogItems.AddRollingStockToCatalogItem
{
    public class AddRollingStockToCatalogItemInputValidatorTests
    {
        private AddRollingStockToCatalogItemInputValidator Validator { get; }

        public AddRollingStockToCatalogItemInputValidatorTests()
        {
            Validator = new AddRollingStockToCatalogItemInputValidator();
        }

        [Fact]
        public void AddRollingStockToCatalogItemInputValidator_ShouldFailToValidate_EmptyInputs()
        {
            var result = Validator.TestValidate(NewAddRollingStockToCatalogItemInput.Empty);

            result.ShouldHaveValidationErrorFor(x => x.Slug);
            result.ShouldHaveValidationErrorFor(x => x.RollingStock.Epoch);
            result.ShouldHaveValidationErrorFor(x => x.RollingStock.Railway);
            result.ShouldHaveValidationErrorFor(x => x.RollingStock.Category);
        }

        [Fact]
        public void AddRollingStockToCatalogItemInputValidator_ShouldPassValidation_WhenInputIsValid()
        {
            var input = NewAddRollingStockToCatalogItemInput.With(
                Slug.Of("acme-123456"),
                NewRollingStockInput.With(
                    category: Category.ElectricLocomotive.ToString(),
                    railway: "fs",
                    epoch: "IV")
            );

            var result = Validator.TestValidate(input);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
