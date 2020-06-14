using System;
using FluentAssertions;
using Xunit;

namespace TreniniDotNet.Domain.Catalog.ValueObjects
{
    public class ItemNumberTests
    {
        [Fact]
        public void ItShouldCreateItemNumberValues()
        {
            var itemNumber = new ItemNumber("123456");
            itemNumber.Value.Should().Be("123456");
        }

        [Fact]
        public void ItShouldCheckWhetherTwoItemNumbersAreEquals()
        {
            var in1 = new ItemNumber("123456");
            var in2 = new ItemNumber("123456");
            (in1.Equals(in2)).Should().BeTrue();
            (in1 == in2).Should().BeTrue();
        }

        [Fact]
        public void ItShouldCheckWhetherTwoItemNumbersAreDifferent()
        {
            var in1 = new ItemNumber("123456");
            var in2 = new ItemNumber("654321");
            (!in1.Equals(in2)).Should().BeTrue();
            (in1 != in2).Should().BeTrue();
        }

        [Fact]
        public void ItShouldProduceAStringRepresentationForItemNumbers()
        {
            var itemNumber = new ItemNumber("123456");
            itemNumber.ToString().Should().Be("123456");
        }

        [Fact]
        public void ItShouldThrowAnExceptionForEmptyStringCreatingNewItemNumbers()
        {
            Action act = () => new ItemNumber("");

            act.Should()
                .Throw<InvalidItemNumberException>()
                .WithMessage("ItemNumber value must be non null and non empty");
        }

        [Fact]
        public void TryCreate_ShouldSuccedsToCreateNewItemNumbers_WhenInputIsNotEmpty()
        {
            bool success = ItemNumber.TryCreate("123456", out var itemNumber);

            success.Should().BeTrue();
            itemNumber.Should().Be(new ItemNumber("123456"));
        }

        [Fact]
        public void TryCreate_ShouldFailToCreateNewItemNumbers_WhenInputIsEmpty()
        {
            bool success = ItemNumber.TryCreate("    ", out var itemNumber);
            success.Should().BeFalse();
        }
    }
}
