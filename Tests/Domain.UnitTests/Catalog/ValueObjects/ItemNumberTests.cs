using Xunit;

namespace TreniniDotNet.Domain.Catalog.ValueObjects
{
    public class ItemNumberTests
    {
        [Fact]
        public void ItShouldCreateItemNumberValues()
        {
            var itemNumber = new ItemNumber("123456");
            Assert.Equal("123456", itemNumber.Value);
        }

        [Fact]
        public void ItShouldCheckWhetherTwoItemNumbersAreEquals()
        {
            var in1 = new ItemNumber("123456");
            var in2 = new ItemNumber("123456");
            Assert.True(in1.Equals(in2));
            Assert.True(in1 == in2);
        }

        [Fact]
        public void ItShouldCheckWhetherTwoItemNumbersAreDifferent()
        {
            var in1 = new ItemNumber("123456");
            var in2 = new ItemNumber("654321");
            Assert.True(!in1.Equals(in2));
            Assert.True(in1 != in2);
        }

        [Fact]
        public void ItShouldProduceAStringRepresentationForItemNumbers()
        {
            var itemNumber = new ItemNumber("123456");
            Assert.Equal("123456", itemNumber.ToString());
        }

        [Fact]
        public void ItShouldThrowAnExceptionForEmptyStringCreatingNewItemNumbers()
        {
            Assert.Throws<InvalidItemNumberException>(() => new ItemNumber(""));
        }
    }
}
