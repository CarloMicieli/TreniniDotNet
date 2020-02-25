using System.Linq;
using System.Collections.Generic;
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

            Assert.Equal(page, result.CurrentPage);
            Assert.Equal(page.Limit, result.Results.Count());
        }

        [Fact]
        public void PaginatedResult_Should_CreateResultWithOnlyNextPageAtTheBeginning()
        {
            var page = new Page(0, 10);
            var data = FakeData(page.Limit + 1);

            var result = new PaginatedResult<string>(page, data);

            Assert.True(result.HasNext);
            Assert.False(result.HasPrevious);
        }

        [Fact]
        public void PaginatedResult_Should_CreateResultWithOnlyPreviousPageAtTheEnd()
        {
            var page = new Page(20, 10);
            var data = FakeData(page.Limit);

            var result = new PaginatedResult<string>(page, data);

            Assert.False(result.HasNext);
            Assert.True(result.HasPrevious);
        }

        [Fact]
        public void PaginatedResult_Should_ReturnNextPageWhenExists()
        {
            var page = new Page(0, 10);
            var data = FakeData(page.Limit + 1);

            var result = new PaginatedResult<string>(page, data);

            var next = result.Next();
            Assert.Equal(page.Next(), next);
        }

        [Fact]
        public void PaginatedResult_Should_ReturnPreviousPageWhenExists()
        {
            var page = new Page(20, 10);
            var data = FakeData(page.Limit);

            var result = new PaginatedResult<string>(page, data);

            var prev = result.Previous();
            Assert.Equal(page.Prev(), prev);
        }

        private List<string> FakeData(int count)
        {
            return Enumerable.Range(0, count)
                .Select(i => $"Value{i}")
                .ToList();
        }
    }
}
