using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using TreniniDotNet.Common.Pagination;
using Xunit;

namespace TreniniDotNet.Domain.Pagination
{
    public class PaginatedResultTests
    {
        [Fact]
        public void PaginatedResult_Should_CreateResultFromPage()
        {
            var page = new Page(0, 10);
            var data = FakeData(page.Limit + 1);

            var result = new PaginatedResult<string>(page, data);

            result.CurrentPage.Should().Be(page);
            result.Results.Should().HaveCount(page.Limit);
        }

        [Fact]
        public void PaginatedResult_Should_CreateResultWithOnlyNextPageAtTheBeginning()
        {
            var page = new Page(0, 10);
            var data = FakeData(page.Limit + 1);

            var result = new PaginatedResult<string>(page, data);

            result.HasNext.Should().BeTrue();
            result.HasPrevious.Should().BeFalse();
        }

        [Fact]
        public void PaginatedResult_Should_CreateResultWithOnlyPreviousPageAtTheEnd()
        {
            var page = new Page(20, 10);
            var data = FakeData(page.Limit);

            var result = new PaginatedResult<string>(page, data);

            result.HasNext.Should().BeFalse();
            result.HasPrevious.Should().BeTrue();
        }

        [Fact]
        public void PaginatedResult_Should_ReturnNextPageWhenExists()
        {
            var page = new Page(0, 10);
            var data = FakeData(page.Limit + 1);

            var result = new PaginatedResult<string>(page, data);

            var next = result.Next();
            next.Should().Be(page.Next());
        }

        [Fact]
        public void PaginatedResult_Should_ReturnPreviousPageWhenExists()
        {
            var page = new Page(20, 10);
            var data = FakeData(page.Limit);

            var result = new PaginatedResult<string>(page, data);

            var prev = result.Previous();
            prev.Should().Be(page.Prev());
        }

        private List<string> FakeData(int count)
        {
            return Enumerable.Range(0, count)
                .Select(i => $"Value{i}")
                .ToList();
        }
    }
}
