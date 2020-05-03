using Xunit;
using FluentAssertions;

namespace TreniniDotNet.Application.Catalog.CatalogItems.EditRollingStock
{
    public class EditRollingStockInputValidatorTests
    {
        private EditRollingStockInputValidator Validator { get; }

        public EditRollingStockInputValidatorTests()
        {
            Validator = new EditRollingStockInputValidator();
        }
    }
}
