using FluentAssertions;
using Xunit;

namespace TreniniDotNet.Domain.Catalog.CatalogItems.RollingStocks
{
    public class CategoryTests
    {
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
            Categories.IsTrain(Category.StarterSet.ToString()).Should().BeTrue();
            Categories.IsTrain(Category.TrainSet.ToString()).Should().BeTrue();

            Categories.IsTrain(Category.DieselLocomotive.ToString()).Should().BeFalse();
            Categories.IsTrain(Category.ElectricLocomotive.ToString()).Should().BeFalse();
            Categories.IsTrain(Category.SteamLocomotive.ToString()).Should().BeFalse();
            Categories.IsTrain(Category.PassengerCar.ToString()).Should().BeFalse();
            Categories.IsTrain(Category.FreightCar.ToString()).Should().BeFalse();
        }
    }
}
