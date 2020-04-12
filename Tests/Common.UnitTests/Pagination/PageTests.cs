using FluentAssertions;
using System;
using Xunit;

namespace TreniniDotNet.Domain.Pagination
{
    public class PageTests
    {
        [Fact]
        public void NextPage_ItShouldCreateTheNextPage_WithTheSameLimit()
        {
            var page = new Page(0, 10);
            var nextPage = page.Next();

            nextPage.Limit.Should().Be(page.Limit);
            nextPage.Start.Should().Be(page.Start + page.Limit);
        }

        [Fact]
        public void PrevPage_ItShouldCreateThePrevPage_WithTheSameLimit()
        {
            var page = new Page(10, 10);
            var prevPage = page.Prev();

            prevPage.Limit.Should().Be(page.Limit);
            prevPage.Start.Should().Be(0);
        }


        [Fact]
        public void PrevPage_ItShouldCreateThePrevPage_WithANonNegativeStart()
        {
            var page = new Page(9, 10);
            var prevPage = page.Prev();

            prevPage.Limit.Should().Be(page.Limit);
            prevPage.Start.Should().Be(0);
        }

        [Fact]
        public void Page_Creation_ShouldThrowAnExceptionForInvalidInputs()
        {
            Action act1 = () => new Page(-1, 10);
            Action act2 = () => new Page(10, -1);

            act1.Should()
                .Throw<ArgumentException>()
                .WithMessage("Invalid page: -1 is not valid, it must be positive (Parameter 'start')");

            act2.Should()
                .Throw<ArgumentException>()
                .WithMessage("Invalid page: -1 is not valid, it must be positive (Parameter 'limit')");
        }

        [Fact]
        public void Page_ToString_ShouldProduceStringRepresentations()
        {
            var page = new Page(9, 10);
            page.ToString().Should().Be("Page(start: 9, limit: 10)");
        }

        [Fact]
        public void Page_Equals_ShouldCheckPageEquality()
        {
            var page1 = new Page(0, 10);
            var page2 = new Page(0, 10);
            var page3 = new Page(10, 10);

            (page1 == page2).Should().BeTrue();
            (page1 == page3).Should().BeFalse();
            (page1 != page2).Should().BeFalse();
            (page1 != page3).Should().BeTrue();
            page1.Equals(page2).Should().BeTrue();
            page1.Equals(page3).Should().BeFalse();
        }
    }
}
