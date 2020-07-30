using System;
using FluentAssertions;
using TreniniDotNet.SharedKernel.Slugs;
using Xunit;

namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    public class ItemNumberTests
    {
        [Fact]
        public void ItemNumber_ShouldCreateNewValueForNotEmptyStrings()
        {
            var itemNumber = new ItemNumber("123456");
            itemNumber.Should().NotBeNull();
            itemNumber.Value.Should().Be("123456");
        }

        [Fact]
        public void ItemNumber_ShouldThrowException_WhenInputIsNull()
        {
            Action act = () =>
            {
                var _ = new ItemNumber(null);
            };
            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void ItemNumber_ShouldThrowException_WhenInputIsBlank()
        {
            Action act = () =>
            {
                var _ = new ItemNumber("");
            };
            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void ItemNumber_ToSlug_ShouldReturnSlugForTheValue()
        {
            var itemNumber = new ItemNumber("123456");
            itemNumber.ToSlug().Should().Be(Slug.Of("123456"));
        }

        [Fact]
        public void ItemNumber_TryCreate_ShouldCreateNewValuesFromValidInputs()
        {
            bool isOk = ItemNumber.TryCreate("123456", out var itemNumber);
            isOk.Should().BeTrue();
            itemNumber.Should().Be(new ItemNumber("123456"));
        }

        [Fact]
        public void ItemNumber_TryCreate_ShouldNotCreateValuesFromInvalidInputs()
        {
            bool created = ItemNumber.TryCreate("", out var itemNumber);
            created.Should().BeFalse();
        }

        [Fact]
        public void ItemNumber_ToString_ShouldProduceStringRepresentations()
        {
            var itemNunmber = new ItemNumber("123456");
            itemNunmber.ToString().Should().Be("123456");
        }

        [Fact]
        public void ItemNumber_ShouldCheckForEquality()
        {
            var itemNumber1 = new ItemNumber("123456");
            var itemNumber2 = new ItemNumber("123456");

            (itemNumber1 == itemNumber2).Should().BeTrue();
            itemNumber1.Equals(itemNumber2).Should().BeTrue();
            (itemNumber1 != itemNumber2).Should().BeFalse();
        }

        [Fact]
        public void ItemNumber_ShouldCheckForInequality()
        {
            var itemNumber1 = new ItemNumber("123456");
            var itemNumber2 = new ItemNumber("654321");

            (itemNumber1 == itemNumber2).Should().BeFalse();
            itemNumber1.Equals(itemNumber2).Should().BeFalse();
            (itemNumber1 != itemNumber2).Should().BeTrue();
        }
    }
}
