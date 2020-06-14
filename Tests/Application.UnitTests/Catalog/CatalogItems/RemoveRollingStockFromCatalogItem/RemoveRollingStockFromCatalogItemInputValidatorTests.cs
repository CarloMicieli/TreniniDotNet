using FluentAssertions;
using Xunit;

namespace TreniniDotNet.Application.Catalog.CatalogItems.RemoveRollingStockFromCatalogItem
{
    public class RemoveRollingStockFromCatalogItemInputValidatorTests
    {
        private RemoveRollingStockFromCatalogItemInputValidator Validator { get; }

        public RemoveRollingStockFromCatalogItemInputValidatorTests()
        {
            Validator = new RemoveRollingStockFromCatalogItemInputValidator();
        }
    }
}
