using Xunit;
using FluentAssertions;
using TreniniDotNet.Common.Lengths;

namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    public class LengthOverBufferTests
    {
        [Fact]
        public void LengthOverBuffer_Create_ShouldCreateValues()
        {
            var lob = LengthOverBuffer.Create(0.65M, 16.5M);

            lob.Should().NotBeNull();
            lob.Inches.Should().Be(Length.OfInches(0.65M));
            lob.Millimeters.Should().Be(Length.OfMillimeters(16.5M));
        }

        [Fact]
        public void LengthOverBuffer_CreateOrDefault_ShouldCreateValues()
        {
            var lob = LengthOverBuffer.CreateOrDefault(0.65M, 16.5M);

            lob.Should().NotBeNull();
            lob.Inches.Should().Be(Length.OfInches(0.65M));
            lob.Millimeters.Should().Be(Length.OfMillimeters(16.5M));
        }

        [Fact]
        public void LengthOverBuffer_CreateOrDefault_ShouldReturnNullWhenBothInputsAreNull()
        {
            var lob = LengthOverBuffer.CreateOrDefault(null, null);
            lob.Should().BeNull();
        }

        [Fact]
        public void LengthOverBuffer_Deconstruct_ShouldExtractValues()
        {
            var lob = LengthOverBuffer.Create(0.65M, 16.5M);

            var (inches, millimeters) = lob;
            inches.Should().Be(Length.OfInches(0.65M));
            millimeters.Should().Be(Length.OfMillimeters(16.5M));
        }

        [Fact]
        public void LengthOverBuffer_Equals_ShouldCheckForEquality()
        {
            var lob1 = LengthOverBuffer.Create(0.65M, 16.5M);
            var lob2 = LengthOverBuffer.Create(0.65M, 16.5M);
            var lob3 = LengthOverBuffer.Create(6.5M, 165.0M);

            (lob1 == lob2).Should().BeTrue();
            lob1.Equals(lob2).Should().BeTrue();
            (lob1 == lob3).Should().BeFalse();
            lob1.Equals(lob3).Should().BeFalse();
        }
    }
}
