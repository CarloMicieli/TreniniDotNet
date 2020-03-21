using Xunit;
using FluentAssertions;

namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    public class CategoryTests
    {
        [Fact]
        public void Category_shouldBeConvertedFromString()
        {
            var cat = Category.ElectricMultipleUnit.ToString().ToCategory();
            cat.Should().Be(Category.ElectricMultipleUnit);
        }

        [Fact]
        public void Category_shouldBFailToConvertedFromInvalidStrings()
        {
            "not-valid".ToCategory().Should().BeNull();
        }

        [Fact]
        public void TryParse_ShouldCreateCategory_FromValidString()
        {
            var success = Categories.TryParse(Category.ElectricMultipleUnit.ToString(), out var cat);
            success.Should().BeTrue();
            cat.Should().Be(Category.ElectricMultipleUnit);
        }

        [Fact]
        public void TryParse_ShouldCreateCategory_FromValidStringIgnoringCase()
        {
            var success = Categories.TryParse(Category.ElectricMultipleUnit.ToString().ToLower(), out var cat);
            success.Should().BeTrue();
            cat.Should().Be(Category.ElectricMultipleUnit);
        }

        [Fact]
        public void TryParse_ShouldFailToCreateCategory_WhenStringIsNotValid()
        {
            var success = Categories.TryParse("   not valid", out var cat);
            success.Should().BeFalse();
        }
    }
}