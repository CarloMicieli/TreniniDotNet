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

            Assert.Equal(page.Limit, nextPage.Limit);
            Assert.Equal(page.Start + page.Limit, nextPage.Start);
        }

        [Fact]
        public void PrevPage_ItShouldCreateThePrevPage_WithTheSameLimit()
        {
            var page = new Page(10, 10);
            var prevPage = page.Prev();

            Assert.Equal(page.Limit, prevPage.Limit);
            Assert.Equal(0, prevPage.Start);
        }


        [Fact]
        public void PrevPage_ItShouldCreateThePrevPage_WithANonNegativeStart()
        {
            var page = new Page(9, 10);
            var prevPage = page.Prev();

            Assert.Equal(page.Limit, prevPage.Limit);
            Assert.Equal(0, prevPage.Start);
        }
    }
}
