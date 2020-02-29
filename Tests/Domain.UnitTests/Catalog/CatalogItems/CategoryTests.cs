using Xunit;

namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    public class CategoryTests
    {
        [Fact]
        public void Category_shouldBeConvertedFromString()
        {
            Assert.Equal(
                Category.ElectricMultipleUnit, 
                Category.ElectricMultipleUnit.ToString().ToCategory());
        }


        [Fact]
        public void Category_shouldBFailToConvertedFromInvalidStrings()
        {
            Assert.Null("not-valid".ToCategory());
        }
    }
}