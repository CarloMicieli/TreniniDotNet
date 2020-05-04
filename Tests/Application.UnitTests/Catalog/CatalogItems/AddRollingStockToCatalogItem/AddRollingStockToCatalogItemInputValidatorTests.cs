using Xunit;
using FluentAssertions;
using FluentValidation.TestHelper;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.CatalogItems;

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
            var result = Validator.TestValidate(CatalogInputs.NewAddRollingStockToCatalogItemInput.Empty);

            result.ShouldHaveValidationErrorFor(x => x.Slug);
            result.ShouldHaveValidationErrorFor(x => x.RollingStock.Epoch);
            result.ShouldHaveValidationErrorFor(x => x.RollingStock.Railway);
            result.ShouldHaveValidationErrorFor(x => x.RollingStock.Category);
        }

        [Fact]
        public void AddRollingStockToCatalogItemInputValidator_ShouldPassValidation_WhenInputIsValid()
        {
            var input = CatalogInputs.NewAddRollingStockToCatalogItemInput.With(
                Slug.Of("acme-123456"),
                CatalogInputs.NewRollingStockInput.With(
                    category: Category.ElectricLocomotive.ToString(),
                    railway: "fs",
                    epoch: "IV")
            );

            var result = Validator.TestValidate(input);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
