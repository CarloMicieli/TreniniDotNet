using FluentAssertions;
using Xunit;

namespace TreniniDotNet.Domain.Catalog.CatalogItems.RollingStocks
{
    public class PrototypeTests
    {
        [Fact]
        public void Prototype_Of_ShouldCreateNewValues()
        {
            var prototype = Prototype.OfLocomotive("E656", "E656 210");

            prototype.Should().NotBeNull();
            prototype.ClassName.Should().Be("E656");
            prototype.RoadNumber.Should().Be("E656 210");
            prototype.Series.Should().BeNullOrEmpty();
        }

        [Fact]
        public void Prototype_Of_ShouldCreateNewValuesWithSeries()
        {
            var prototype = Prototype.OfLocomotive("E656", "E656 210", "I");

            prototype.Should().NotBeNull();
            prototype.ClassName.Should().Be("E656");
            prototype.RoadNumber.Should().Be("E656 210");
            prototype.Series.Should().Be("I");
        }

        [Fact]
        public void Prototype_ToString_ShouldProduceStringRepresentations()
        {
            var prototype1 = Prototype.OfLocomotive("E656", "E656 210");
            var prototype2 = Prototype.OfLocomotive("E656", "E656 210", "II");

            prototype1.ToString().Should().Be("Prototype(E656, E656 210)");
            prototype2.ToString().Should().Be("Prototype(E656, E656 210, II)");
        }
    }
}
