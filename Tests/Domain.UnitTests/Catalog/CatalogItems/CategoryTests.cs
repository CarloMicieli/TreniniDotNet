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

        [Fact]
        public void Categories_ShouldCheck_WhenACategoryIsForALocomotive()
        {
            Categories.IsLocomotive(Category.DieselLocomotive.ToString()).Should().BeTrue();
            Categories.IsLocomotive(Category.ElectricLocomotive.ToString()).Should().BeTrue();
            Categories.IsLocomotive(Category.SteamLocomotive.ToString()).Should().BeTrue();

            Categories.IsLocomotive(Category.PassengerCar.ToString()).Should().BeFalse();
            Categories.IsLocomotive(Category.FreightCar.ToString()).Should().BeFalse();
            Categories.IsLocomotive(Category.TrainSet.ToString()).Should().BeFalse();
            Categories.IsLocomotive(Category.Railcar.ToString()).Should().BeFalse();
            Categories.IsLocomotive(Category.ElectricMultipleUnit.ToString()).Should().BeFalse();
        }

        [Fact]
        public void Categories_ShouldCheck_WhenACategoryIsForATrain()
        {
            Categories.IsTrain(Category.Railcar.ToString()).Should().BeTrue();
            Categories.IsTrain(Category.ElectricMultipleUnit.ToString()).Should().BeTrue();

            Categories.IsTrain(Category.DieselLocomotive.ToString()).Should().BeFalse();
            Categories.IsTrain(Category.ElectricLocomotive.ToString()).Should().BeFalse();
            Categories.IsTrain(Category.SteamLocomotive.ToString()).Should().BeFalse();
            Categories.IsTrain(Category.PassengerCar.ToString()).Should().BeFalse();
            Categories.IsTrain(Category.FreightCar.ToString()).Should().BeFalse();
            Categories.IsTrain(Category.TrainSet.ToString()).Should().BeFalse();

        }
    }
}