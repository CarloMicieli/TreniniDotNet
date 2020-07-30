using FluentAssertions;
using TreniniDotNet.TestHelpers.SeedData.Collecting;
using Xunit;

namespace TreniniDotNet.Web.Collecting.V1.Wishlists.Common.ViewModels
{
    public class WishlistInfoViewTests
    {
        [Fact]
        public void WishlistInfoView_ShouldRenderWishlists()
        {
            var wishlist = CollectingSeedData.Wishlists.GeorgeFirstList();

            var view = new WishlistInfoView(wishlist);

            view.Id.Should().Be(wishlist.Id);
            view.Visibility.Should().Be("Private");
            view.Slug.Should().Be("george-first-list");
            view.ListName.Should().Be("First list");
        }
    }
}