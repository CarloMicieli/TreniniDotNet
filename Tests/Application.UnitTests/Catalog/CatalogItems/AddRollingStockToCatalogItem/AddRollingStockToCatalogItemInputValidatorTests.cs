using Xunit;
using FluentAssertions;

namespace TreniniDotNet.Application.Catalog.CatalogItems.AddRollingStockToCatalogItem
{
    public class AddRollingStockToCatalogItemInputValidatorTests
    {
        private AddRollingStockToCatalogItemInputValidator Validator { get; }

        public AddRollingStockToCatalogItemInputValidatorTests()
        {
            Validator = new AddRollingStockToCatalogItemInputValidator();
        }
    }
}
